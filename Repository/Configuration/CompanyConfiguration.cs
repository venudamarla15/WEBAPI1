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
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasData
                (
                new Company
                {
                    Id = new Guid("F8B233E0-4A5C-4E28-91DE-F6168FA4FAEA"),
                    Name = "IT Solutions_ ltd",
                    Address = "583 Wall Dr. Gwynn Oak, MD 21207",
                    Country = "USA"
                },
                new Company
                {
                    Id = new Guid ("D08A779F-A881-4686-9C59-0CF738A44800"),
                    Name = "Admin Solutions Ltd",
                    Address = "312 Forest Avenue, BF 923",
                    Country = "USA"
                }
                );
            
        }
    }
}
