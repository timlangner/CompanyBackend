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

        // GET api/departments/
        [HttpGet]
        public IActionResult GetDepartments()
        {
            if (_departmentInterface.Read().Count == 0)
            {
                return NoContent();
            }
            return Ok(_departmentInterface.Read());
        }

        // GET api/departments/1/
        [HttpGet("{id}")]
        public IActionResult GetDepartment(int id)
        {
            if (_departmentInterface.Read(id) == null)
            {
                return NoContent();
            }

            return Ok(_departmentInterface.Read(id));
        }

        // POST api/departments/
        [HttpPost]
        public IActionResult CreateDepartment([FromBody] DepartmentDto departmentDto)
        {
            bool retval = _departmentInterface.Create(departmentDto);

            if (departmentDto == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            return StatusCode(StatusCodes.Status201Created);
        }

        //PUT api/departments/2/
        [HttpPut("{id}")]
        public IActionResult UpdateDepartment(int id, [FromBody] DepartmentDto departmentDto)
        {
            //Check if user put invalid requests
            if (id <= 0)
            {
                return BadRequest();
            }

            bool retval = _departmentInterface.Update(id, departmentDto);

            if (retval == false)
            {
                return Conflict();
            }

            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
