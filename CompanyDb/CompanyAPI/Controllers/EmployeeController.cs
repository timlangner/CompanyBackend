using CompanyAPI.Helper;
using CompanyAPI.Interface;
using CompanyAPI.Model;
using CompanyAPI.Model.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chayns.Auth.ApiExtensions;
using Chayns.Auth.Shared.Constants;

namespace CompanyAPI.Controllers
{
    [Route("/api/employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IBaseInterface<Employee, EmployeeDto> _employeeRepository;

        public EmployeeController(IBaseInterface<Employee, EmployeeDto> employeeRepository, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<EmployeeController>();
            _employeeRepository = employeeRepository;
        }

        // GET api/employees/
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var retval = await _employeeRepository.Read();
            if (retval.Count() == 0)
            {
                return NoContent();
            }
            return Ok(retval);
        }

        // GET api/employees/1/
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var result = await _employeeRepository.Read(id);
            if (result == null)
            {
                return NoContent();
            }

            return Ok(result);
        }

        // POST api/employees/
        [HttpPost]
        [ChaynsAuth]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeDto employeeDto)
        {
            bool retval = await _employeeRepository.Create(employeeDto);

            if (employeeDto == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            _logger.LogInformation("Employee created.");
            return StatusCode(StatusCodes.Status201Created);
        }

        //PUT api/employees/5/
        [HttpPut("{id}")]
        [ChaynsAuth]
        public async Task<IActionResult> UpdateCompany(int id, [FromBody] EmployeeDto employeeDto)
        {
            //Check if user put invalid requests
            if (id <= 0)
            {
                _logger.LogInformation("Invalid request. The ID is smaller or equal zero.");
                return BadRequest();
            }

            bool retval = await _employeeRepository.Update(id, employeeDto);

            if (retval == false)
            {
                return Conflict();
            }

            _logger.LogInformation("Employee updated.");
            return StatusCode(StatusCodes.Status200OK);
        }

        // DELETE api/employees/2/
        [HttpDelete("{id}")]
        [ChaynsAuth]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            bool retval = await _employeeRepository.Delete(id);

            if (retval == false)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            _logger.LogInformation("Employee deleted.");
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
