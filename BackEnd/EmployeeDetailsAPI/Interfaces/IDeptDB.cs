using EmployeeDetailsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeDetailsAPI.Interfaces
{
    public interface IDeptDB
    {
        object getData();
        Department getData(int id);
        void postData(Department employee);
        void putData(int id, Department employee);
        void delData(int id);
    }
}
