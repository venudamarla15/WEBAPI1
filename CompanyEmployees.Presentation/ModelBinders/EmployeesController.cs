using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
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
        public async Task<IActionResult> GetEmployeesForCompany(Guid companyId)
        {
            var employees = await _service.EmployeeService.GetEmployees(companyId, trackChanges:false);
            return Ok(employees);
        }


        [HttpGet("{id:guid}", Name = "GetEmployeeForCompany")]
        public async Task<IActionResult> GetEmployeesForComapny(Guid companyId, Guid id)
        {
            var employee = await _service.EmployeeService.GetEmployee(companyId, id,trackChanges: false);
            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployeeForCompany(Guid companyId, [FromBody]EmployeeForCreationDto employee)
        {
            if(employee is null)
            {
                return BadRequest("EmployeeForCreationDto object is null");

            }
            if(!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            var employeeToReturn = await _service.EmployeeService.CreateEmployeeForCompany(companyId, employee, trackChanges:false);

            return CreatedAtRoute("GetEmployeeForCompany", new { companyId, Id = employeeToReturn.Id },employeeToReturn);
        }


        [HttpDelete("{id:guid}")]

        public async Task<IActionResult> DeleteEmployeeForCompany(Guid companyId, Guid id)
        {
            await _service.EmployeeService.DeleteEmployee(companyId, id,trackChanges:false);

            return NoContent();
        }

        [HttpPut("{id:guid}")]

        public async Task<IActionResult> UpdateEmployeeForCompany(Guid companyId, Guid id, [FromBody] EmployeeForUpdateDto employeeForUpdateDto)
        {
            if (employeeForUpdateDto is null)
                return BadRequest("EmpoyeeForUpdatDto object is null");

            if(!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            await _service.EmployeeService.UpdateEmployeeForCompany(companyId, id, employeeForUpdateDto, compTrackChanges: false, empTrackChanges: true);

            return NoContent();
        }
    }

   
}
