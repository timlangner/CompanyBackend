﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CompanyAPI.Interface;
using CompanyAPI.Model;
using CompanyAPI.Model.Dto;
using CompanyAPI.Repository;
using Microsoft.AspNetCore.Http;

namespace CompanyAPI.Controller
{
    [Route("/api/companies")]
    public class CompanyController : ControllerBase
    {
        readonly IBaseInterface<Company, CompanyDto> 
        _companyRepository = new CompanyRepository();

        // GET companies
        [HttpGet]
        public IActionResult GetCompanies()
        {
            if (_companyRepository.Read().Count == 0)
            {
                return NoContent();
            }
            return Ok(_companyRepository.Read());
        }

        // GET companies/1
        [HttpGet("{id}")]
        public IActionResult GetCompany(int id)
        {
            if (_companyRepository.Read(id) == null)
            {
                return NoContent();
            }

            return Ok(_companyRepository.Read(id));
        }

        // POST api/values
        [HttpPost]
        public IActionResult PostCompany([FromBody] CompanyDto companyDto)
        {
            bool retval = _companyRepository.Create(companyDto);

            if (companyDto == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            return StatusCode(StatusCodes.Status201Created);
        }

        //PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult PutCompany(int id, [FromBody] CompanyDto companyDto)
        {
            //Check if user put invalid requests
            if (id >= 0)
            {
                return BadRequest();
            }

            bool retval = _companyRepository.Update(id, companyDto);

            if (retval == false)
            {
                return Conflict();
            }

            return StatusCode(StatusCodes.Status200OK);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCompany(int id)
        {
            bool retval = _companyRepository.Delete(id);

            if (retval == false)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
