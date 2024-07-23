using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasData
                (
                    new Employee
                    {
                        Id = new Guid("3639D909-6DEA-4778-8B33-4B903022EB48"),
                        Name = "Venu",
                        Age = 32,
                        Position = "Software Developer",
                        CompanyId = new Guid("F8B233E0-4A5C-4E28-91DE-F6168FA4FAEA")
                    },
                    new Employee
                    {
                        Id = new Guid("32936D94-AE25-47AE-A4A9-96C203B69824"),
                        Name = "Sai Saranya",
                        Age = 28,
                        Position = "Software Developer",
                        CompanyId = new Guid("D08A779F-A881-4686-9C59-0CF738A44800")
                    },
                    new Employee
                    {
                        Id = new Guid("C89D1107-9E1E-47D4-8F7C-9F7BA07899F6"),
                        Name = "Ravi",
                        Age = 29,
                        Position = "FullStack dot Net Developer",
                        CompanyId = new Guid("D08A779F-A881-4686-9C59-0CF738A44800")
                    }



                );
        }
    }
}
