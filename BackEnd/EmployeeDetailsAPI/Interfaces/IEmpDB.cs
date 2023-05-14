using EmployeeDetailsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeDetailsAPI.Interfaces
{
    public interface IEmpDB
    {
        object getData();
        Employee getData(int id);
        void postData(Employee employee);
        void putData(int id, Employee employee);
        void delData(int id);
    }
}
