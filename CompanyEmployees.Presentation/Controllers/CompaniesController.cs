﻿using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

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
        public IActionResult GetCompanies()
        {
            //throw new  Exception("Exception");
            var companies =
                _service.CompanyService.GetAllCompanies(trackChanges: false);
            return Ok(companies);
           
        }
        [HttpGet("{id:guid}")]
        public IActionResult GetCompany(Guid id)
        {
            var company = _service.CompanyService.GetCompany(id, trackChanges: false);
            return Ok(company);
        }
    }

}
