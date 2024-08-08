﻿using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyEmployees.Presentation.Controllers
{
    [Route("api/companies/{companyId}/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IServiceManager _service;
        public EmployeesController(IServiceManager servive) => _service = servive;

        [HttpGet]
        public IActionResult GetEmployeesForCompany(Guid companyId)
        {
            var employees = _service.EmployeeService.GetEmployees(companyId, trackChanges:false);
            return Ok(employees);
        }


        [HttpGet("{id:guid}")]
        public IActionResult GetEmployeesForComapny(Guid companyId, Guid id)
        {
            var employee = _service.EmployeeService.GetEmployee(companyId, id,trackChanges: false);
            return Ok(employee);
        }
    }

   
}
