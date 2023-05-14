using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeDetailsAPI.Interfaces;
using EmployeeDetailsAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeDetailsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IRegAndLogin _db;
        private readonly IEmpDB _empdb;
        private readonly List<Employee> _empList;
        public RegisterController(IRegAndLogin db, IEmpDB empdb)
        {
            _db = db;
            _empdb = empdb;
            _empList = _empdb.getData() as List<Employee>;
        }
        [HttpPost]
        public bool Post([FromBody] Register user)
        {
            for(int i = 0; i < _empList.Count; i++)
            {
                if(_empList[i].Name == user.username)
                {
                    user.empId = _empList[i].EMPID;
                    if(_db.GetHash(_empList[i].Name) != null)
                    {
                        _db.updateHash(user);
                    }
                    else
                    {
                        _db.SetHash(user);
                    }
                    return true;
                }
            }
            return false;
        }
    }
}