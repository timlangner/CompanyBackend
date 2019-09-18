using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CompanyAPI.Interface;
using CompanyAPI.Model;
using CompanyAPI.Model.Dto;
using CompanyAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace CompanyAPI.Controller
{
    [Route("/api/companies")]
    public class CompanyController : ControllerBase
    {
        private readonly ILogger<CompanyController> _logger;
        private readonly IBaseInterface<Company, CompanyDto> _companyRepository;

        public CompanyController(IBaseInterface<Company, CompanyDto> companyRepository, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<CompanyController>();
            _companyRepository = companyRepository;
        }

        // GET api/companies/
        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            var retval = await _companyRepository.Read();
            if (retval.Count() == 0)
            {
                return NoContent();
            }
            return Ok(retval);
        }

        // GET api/companies/1/
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompany(int id)
        {
            try
            {
                _logger.LogInformation($"Hello from {Request.Headers["User-Agent"]}");
                if (await _companyRepository.Read(id) == null)
                {
                    return NoContent();
                }

                return Ok(_companyRepository.Read(id));
            }catch(SqlException ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable);
            }
            catch (Helper.RepoException repoEx)
            {
                switch (repoEx.ExType)
                {
                    case Helper.RepoResultType.SQLERROR:
                        _logger.LogError(repoEx.InnerException, repoEx.Message);
                        return StatusCode(StatusCodes.Status503ServiceUnavailable);
                }

                return StatusCode(StatusCodes.Status503ServiceUnavailable);
            }
        }

        // POST api/companies/
        [HttpPost]
        public async Task<IActionResult> PostCompany([FromBody] CompanyDto companyDto)
        {
            bool retval = await _companyRepository.Create(companyDto);

            if (companyDto == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            return StatusCode(StatusCodes.Status201Created);
        }

        //PUT api/companies/5/
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany(int id, [FromBody] CompanyDto companyDto)
        {
            //Check if user put invalid requests
            if (id <= 0)
            {
                return BadRequest();
            }

            bool retval = await _companyRepository.Update(id, companyDto);

            if (retval == false)
            {
                return Conflict();
            }

            return StatusCode(StatusCodes.Status200OK);
        }

        // DELETE api/companies/2/
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            bool retval = await _companyRepository.Delete(id);

            if (retval == false)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
