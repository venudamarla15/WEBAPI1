using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Company
    {
        [Column("CompanyId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage ="Company Name is Requires")]
        [MaxLength(60,ErrorMessage ="Max length for the Name is 60 Characters.")]
        public string? Name { get; set; }

        [Required(ErrorMessage ="Company address is required.")]
        [MaxLength(60, ErrorMessage ="Max length for the address is 60 Characters.")]
        public string? Address { get; set; }
        public string? Country { get; set; }

        public ICollection<Employee>? Employees { get; set; }
    }
}
