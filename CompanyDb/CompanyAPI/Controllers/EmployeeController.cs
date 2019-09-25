using CompanyAPI.Interface;
using CompanyAPI.Model;
using CompanyAPI.Model.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

    }
}
