using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeDetailsAPI.Models
{
    public class Register
    {
        public string username { get; set; }
        public string passwordHash { get; set; }
        public string empId { get; set; }
    }
}
