using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public sealed class EmployeeService : IEmployeeService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _loggerManager;
        private readonly AutoMapper.IMapper _mapper;

        public EmployeeService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _loggerManager = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployees(Guid companyId, bool trackChanges)
        {
            var company = await _repository.Company.GetCompany(companyId, trackChanges);
            if (company is null)
            {
                throw new CompanyNotFoundException(companyId);
            }
            var employeesFromDb = await _repository.Employee.GetEmployees(companyId, trackChanges);
           
            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeesFromDb); 
            
            return employeesDto;
        }

        public async Task<EmployeeDto> GetEmployee(Guid companyId, Guid id, bool trackChanges)
        {
            var company = await _repository.Company.GetCompany(companyId, trackChanges);
            if(company is null)
            {
                throw new CompanyNotFoundException(companyId);
            }

            var employeesFromDb = await _repository.Employee.GetEmployee(companyId, id, trackChanges); 
            if(employeesFromDb is null)
            {
                throw new EmployeeNotFoundException(id);
            }
            var employee = _mapper.Map<EmployeeDto>(employeesFromDb);
            return employee;
        }

        public async Task<EmployeeDto> CreateEmployeeForCompany(Guid companyId, EmployeeForCreationDto employeeForCreation, bool trackChanges)
        {
            var company = await  _repository.Company.GetCompany(companyId, trackChanges);
            if (company is null)
                throw new CompanyNotFoundException(companyId);
            var employeeEntity = _mapper.Map<Employee>(employeeForCreation);
            _repository.Employee.CreateEmployeeForCompany(companyId, employeeEntity);
            await _repository.Save();
            var employeeToReturn = _mapper.Map<EmployeeDto>(employeeEntity);
            return employeeToReturn;
        }

        public async Task DeleteEmployee(Guid companyId, Guid id, bool trackChanges)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges);
            if(company is null)
                throw new CompanyNotFoundException(companyId);
            var employeeForCompany = await _repository.Employee.GetEmployee(companyId, id, trackChanges);
            if(employeeForCompany is null)
                throw new EmployeeNotFoundException(id);
             await _repository.Employee.DeleteEmployee(employeeForCompany);
            await _repository.Save();
        }

        public async Task UpdateEmployeeForCompany(Guid companyId, Guid id, EmployeeForUpdateDto employeeForUpdate, bool compTrackChanges, bool empTrackChanges)
        {
            var company = await _repository.Company.GetCompany(companyId, compTrackChanges);
            if(company is null)
                throw new CompanyNotFoundException(companyId);

            var employeeEntity = await _repository.Employee.GetEmployee(companyId,id, empTrackChanges);
            if(employeeEntity is null)
             throw new EmployeeNotFoundException(id);
            
            _mapper.Map(employeeForUpdate, employeeEntity);
            await _repository.Save();
        }
    }
}
