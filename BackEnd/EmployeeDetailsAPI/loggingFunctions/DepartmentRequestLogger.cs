﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeDetailsAPI.loggingFunctions
{
    public class DepartmentRequestLogger
    {
        private static string filePath = @"C:\Users\RitvikS\Desktop\Tasks\EmployeeDetailsApp\BackEnd\EmployeeDetailsAPI\logs\requests\deptreqlogger.txt";
        public void logRequest(string msg)
        {
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(DateTime.Now + " " + msg);
            }
        }
    }
}
