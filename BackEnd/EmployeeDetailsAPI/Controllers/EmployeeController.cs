using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeDetailsAPI.Interfaces;
using EmployeeDetailsAPI.loggingFunctions;
using EmployeeDetailsAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EmployeeDetailsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmpDB _db;
        private readonly static EmployeeRequestLogger _empreqlog = new EmployeeRequestLogger();
        public EmployeeController(IEmpDB db)
        {
            _db = db;
        }
        // GET api/employee
        [HttpGet]
        public ActionResult<IEnumerable<Employee>> Get()
        {
            _empreqlog.logRequest("GET ALL");
            List<Employee> data = _db.getData() as List<Employee>;
            if (data is null)
            {
                return NotFound();
            }
            return Ok(_db.getData());
        }

        // GET api/employee/2
        [HttpGet("{id}")]
        public ActionResult<Employee> Get(int id)
        {
            _empreqlog.logRequest("GET SINGLE");
            Employee data = _db.getData(id);
            if(data is null)
            {
                //return NotFound();
                return StatusCode(404);
            }
            return Ok(_db.getData(id));
        }

        // POST api/employee
        [HttpPost]
        public void Post([FromBody] Employee employee)
        {
            _empreqlog.logRequest("POST");
            _db.postData(employee);
        }

        // PUT api/employee/2
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Employee employee)
        {
            _empreqlog.logRequest("PUT");
            _db.putData(id, employee);
        }

        // DELETE api/employee/2
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _empreqlog.logRequest("DELETE");
            _db.delData(id);
        }
    }
}
