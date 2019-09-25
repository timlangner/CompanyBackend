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
using CompanyAPI.Helper;

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
            var result = await _companyRepository.Read(id);
            if (result == null)
            {
                return NoContent();
            }

            return Ok(result);
        }

        // POST api/companies/
        [HttpPost]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyDto companyDto)
        {
            var user = Auth.GetUser(HttpContext);
            if (user.TobitUserID == 2105910)
            {
                bool retval = await _companyRepository.Create(companyDto);

                if (companyDto == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }

                return StatusCode(StatusCodes.Status201Created);
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
        }

        //PUT api/companies/5/
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(int id, [FromBody] CompanyDto companyDto)
        {
            //Check if user put invalid requests
            if (id <= 0)
            {
                return BadRequest();
            }

            var user = Auth.GetUser(HttpContext);
            if (user.TobitUserID == 2105910)
            {
                bool retval = await _companyRepository.Update(id, companyDto);

                if (retval == false)
                {
                    return Conflict();
                }

                return StatusCode(StatusCodes.Status200OK);
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }

        }

        // DELETE api/companies/2/
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            var user = Auth.GetUser(HttpContext);
            if (user.TobitUserID == 2105910)
            {
                bool retval = await _companyRepository.Delete(id);

                if (retval == false)
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }

                return StatusCode(StatusCodes.Status204NoContent);
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
        }
    }
}
