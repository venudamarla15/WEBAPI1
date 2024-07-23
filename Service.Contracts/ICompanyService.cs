namespace Service.Contracts
{
   public interface ICompanyService
    {

    }

    public interface IEmployeeService
    {

    }

    public interface IServiceManager
    {
        ICompanyService CompanyService { get; }
        IEmployeeService EmployeeService { get; }
    }
}
