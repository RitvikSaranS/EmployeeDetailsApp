﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeDetailsAPI.Models
{
    public class Login
    {
        public string username { get; set; }
        public string passwordHash { get; set; }
    }
}
