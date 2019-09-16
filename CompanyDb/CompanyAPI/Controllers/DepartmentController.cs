using CompanyAPI.Interface;
using CompanyAPI.Model;
using CompanyAPI.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyAPI.Controllers
{
    [Route("/api/departments")]
    public class DepartmentController : ControllerBase
    {
        private readonly IBaseInterface<Department, DepartmentDto> _departmentInterface;

        public DepartmentController(IBaseInterface<Department, DepartmentDto> departmentInterface)
        {
            _departmentInterface = departmentInterface;
        }

        // GET departments
        [HttpGet]
        public IActionResult GetCompanies()
        {
            if (_departmentInterface.Read().Count == 0)
            {
                return NoContent();
            }
            return Ok(_departmentInterface.Read());
        }
    }
}
