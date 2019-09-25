using CompanyAPI.Helper;
using CompanyAPI.Interface;
using CompanyAPI.Model;
using CompanyAPI.Model.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeDto employeeDto)
        {
            var uacGroups = Auth.GetUACGroupFromSite(HttpContext);
            if (uacGroups)
            {
                bool retval = await _employeeRepository.Create(employeeDto);

                if (employeeDto == null)
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

        //PUT api/employees/5/
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(int id, [FromBody] EmployeeDto employeeDto)
        {
            //Check if user put invalid requests
            if (id <= 0)
            {
                return BadRequest();
            }

            var uacGroups = Auth.GetUACGroupFromSite(HttpContext);
            if (uacGroups)
            {
                bool retval = await _employeeRepository.Update(id, employeeDto);

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

        // DELETE api/employees/2/
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var uacGroups = Auth.GetUACGroupFromSite(HttpContext);
            if (uacGroups)
            {
                bool retval = await _employeeRepository.Delete(id);

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
