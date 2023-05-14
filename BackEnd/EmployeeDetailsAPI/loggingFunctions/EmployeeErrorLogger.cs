﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeDetailsAPI.loggingFunctions
{
    public class EmployeeErrorLogger
    {
        private static string filePath = @"C:\Users\RitvikS\Desktop\Tasks\EmployeeDetailsApp\BackEnd\EmployeeDetailsAPI\logs\errors\emperrorlogger.txt";
        public void logError(string msg)
        {
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(DateTime.Now + " " + msg);
            }
        }
        
    }
}
