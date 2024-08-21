using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    //public class EmployeeForUpdateDto
    //{
    //    [Required(ErrorMessage ="Employee Name is required")]
    //    [MaxLength(30, ErrorMessage ="Max 30 characters are allowed")]
    //    public string Name { get; set; }

    //    [Range(18, int.MaxValue,ErrorMessage ="Age is required and it can't be lower than 18!")]
    //    public int Age { get; set; }

    //    [Required(ErrorMessage ="position is required field")]
    //    [MaxLength(20, ErrorMessage ="Max of 20 characters allowed")]
    //    public string? Position { get; set; }
    //}
    public record EmployeeForUpdateDto : EmployeeForManipulationDto;

}
