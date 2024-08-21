﻿using CompanyEmployees.Presentation.ModelBinders;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace CompanyEmployees.Presentation.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IServiceManager _service;
        //private readonly ICompanyRepository _company;
        public CompaniesController(IServiceManager service)
        {
            _service = service;
            // _company = company;
        }
        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            //throw new  Exception("Exception");
            var companies =
                await _service.CompanyService.GetAllCompanies(trackChanges: false);
            return Ok(companies);

        }
        [HttpGet("{id:guid}", Name = "CompanyId")]
        public async Task<IActionResult> GetCompany(Guid id)
        {
            var company = await _service.CompanyService.GetCompany(id, trackChanges: false);
            return Ok(company);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyForCreationDto company)
        {

            if (company is null)
            {
                return BadRequest("CompanyForCreationDto object is null");
            }
            if(!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            var companyCreated = await _service.CompanyService.CreateCompany(company);

            return CreatedAtRoute("CompanyId", new { id = companyCreated.id }, companyCreated);
        }

        [HttpGet("Collection/({ids}", Name = "CompanyCollection")]
        public async Task<IActionResult> GetCompanyCollectons([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            var companies = await _service.CompanyService.GetByIds(ids, trackChanges: false);

            return Ok(companies);
        }

        [HttpPost("collection")]
        public async Task<IActionResult> CreateCompanyCollection([FromBody] IEnumerable<CompanyForCreationDto> companyCollection)
        {
            var result = await _service.CompanyService.CreateCompanyCollection(companyCollection);
            return CreatedAtRoute("CompanyCollection", new { result.ids }, result.companies);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCompany(Guid id)
        {
            await _service.CompanyService.DeleteCompany(id, trackChanges: false);

            return NoContent();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateCompany(Guid id, [FromBody] CompanyForUpdateDto company)
        {
            if (company is null)
                return BadRequest("CompanyForUpdateDto object is null");
            await _service.CompanyService.UpdateCompany(id, company, trackChanges: true);
            return NoContent();
        }
    }

}
