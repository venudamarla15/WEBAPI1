using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyDto>> GetAllCompanies(bool trackChanges);
        Task<CompanyDto> GetCompany(Guid companyId, bool trackChanges);
        Task<CompanyDto> CreateCompany(CompanyForCreationDto company);
        Task<IEnumerable<CompanyDto>> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
        Task<(IEnumerable<CompanyDto> companies, string ids)> CreateCompanyCollection
            (IEnumerable<CompanyForCreationDto> companyCollection);
        Task DeleteCompany(Guid companyId, bool trackChanges);
        Task UpdateCompany(Guid companyId, CompanyForUpdateDto companyForUpdateDto, bool trackChanges);


    }

    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetEmployees(Guid companyId, bool trackChanges);
        Task<EmployeeDto> GetEmployee(Guid companyId, Guid id, bool trackChanges);
        Task<EmployeeDto> CreateEmployeeForCompany(Guid companyId, EmployeeForCreationDto employeeForCreation, bool trackChanges);

        Task DeleteEmployee(Guid companyId, Guid id, bool trackChanges);
        Task UpdateEmployeeForCompany(Guid companyId, Guid id, EmployeeForUpdateDto employeeForUpdate, bool compTrackChanges, bool empTrackChanges);

    }

    public interface IServiceManager
    {
        ICompanyService CompanyService { get; }
        IEmployeeService EmployeeService { get; }
    }
}
