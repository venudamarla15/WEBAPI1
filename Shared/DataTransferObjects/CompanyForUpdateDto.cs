﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public class CompanyForUpdateDto
    {
        public  string  Name { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }

        IEnumerable<EmployeeForCreationDto> Employees { get; set; }
    }
}
