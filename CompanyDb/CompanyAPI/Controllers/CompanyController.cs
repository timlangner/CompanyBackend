using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CompanyAPI.Interface;
using CompanyAPI.Model;
using CompanyAPI.Model.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Chayns.Auth.ApiExtensions;

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
            if (!retval.Any())
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
        [HttpPost, ChaynsAuth]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyDto companyDto)
        {
            await _companyRepository.Create(companyDto);

            return StatusCode(companyDto == null ? StatusCodes.Status400BadRequest : StatusCodes.Status201Created);
        }

        //PUT api/companies/5/
        [HttpPut("{id}"), ChaynsAuth]
        public async Task<IActionResult> UpdateCompany(int id, [FromBody] CompanyDto companyDto)
        {
            //Check if user put invalid requests
            if (id <= 0)
            {
                return BadRequest();
            }

            var retval = await _companyRepository.Update(id, companyDto);

            return retval == false ? Conflict() : StatusCode(StatusCodes.Status200OK);
        }

        // DELETE api/companies/2/
        [HttpDelete("{id}"), ChaynsAuth]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            var retval = await _companyRepository.Delete(id);

            return StatusCode(retval == false ? StatusCodes.Status400BadRequest : StatusCodes.Status204NoContent);
        }
    }
}
