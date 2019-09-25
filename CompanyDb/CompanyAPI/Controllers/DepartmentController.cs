using CompanyAPI.Interface;
using CompanyAPI.Model;
using CompanyAPI.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CompanyAPI.Helper;

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
        public async Task<IActionResult> GetDepartments()
        {
            var retval = await _departmentInterface.Read();

            if (retval.Count == 0)
            {
                return NoContent();
            }
            return Ok(retval);
        }

        // GET api/departments/1/
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartment(int id)
        {
            if (await _departmentInterface.Read(id) == null)
            {
                return NoContent();
            }

            return Ok(_departmentInterface.Read(id));
        }

        // POST api/departments/
        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromBody] DepartmentDto departmentDto)
        {
            var uacGroups = Auth.GetUACGroupFromSite(HttpContext);
            if (uacGroups)
            {
                bool retval = await _departmentInterface.Create(departmentDto);

                if (departmentDto == null)
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

        //PUT api/departments/2/
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] DepartmentDto departmentDto)
        {
            //Check if user put invalid requests
            if (id <= 0)
            {
                return BadRequest();
            }

            var uacGroups = Auth.GetUACGroupFromSite(HttpContext);
            if (uacGroups)
            {
                bool retval = await _departmentInterface.Update(id, departmentDto);

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

        // DELETE api/departments/2/
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var uacGroups = Auth.GetUACGroupFromSite(HttpContext);
            if (uacGroups)
            {
                bool retval = await _departmentInterface.Delete(id);

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
