using EmployeeDetailsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeDetailsAPI.Interfaces
{
    public interface IRegAndLogin
    {
        string GetHash(string username);
        void SetHash(Register user);
        void updateHash(Register user);
    }
}
