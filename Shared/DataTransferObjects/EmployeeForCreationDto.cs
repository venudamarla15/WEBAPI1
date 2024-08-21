using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    //public class EmployeeForCreationDto 
    //{

    //    [Required(ErrorMessage = "Employee Name is Required")]
    //    [MaxLength(30, ErrorMessage ="Max length 30 characters are allaowed")]
    //    public string  Name { get; set; }

    //    [Range(18, int.MaxValue, ErrorMessage ="Age is requied and it can't be lower than 18")]
    //    public int  Age { get; set; }

    //    [Required(ErrorMessage ="Postion is a mandatory Field")]
    //    [MaxLength(20, ErrorMessage ="Max 20 characters are allowed")]
    //    public string Position { get; set; }
    //}

    public record EmployeeForCreationDto : EmployeeForManipulationDto;
}
