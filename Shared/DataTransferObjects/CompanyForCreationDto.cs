using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public class CompanyForCreationDto
    {
        [Required(ErrorMessage ="Name required ")]
        [MaxLength(30, ErrorMessage ="Max 30 characters are allowed")]
        public  string Name { get; set; }

        [Required(ErrorMessage ="Address is required")]
        [MaxLength(100, ErrorMessage ="max 100 characters are allowed")]
        public  string Address { get; set; }
        [Required(ErrorMessage ="Country is mandatory")]
        [MaxLength(5, ErrorMessage ="Max 5 characters are allowed")]
        public  string Country { get; set; }
        IEnumerable<EmployeeForCreationDto> Employees { get; set; }
    }
}
