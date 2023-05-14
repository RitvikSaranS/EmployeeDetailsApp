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
    public class DepartmentController : ControllerBase
    {
        private readonly IDeptDB _db;
        private readonly static DepartmentRequestLogger _deptreqlog = new DepartmentRequestLogger();
        public DepartmentController(IDeptDB db)
        {
            _db = db;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            _deptreqlog.logRequest("GET ALL");
            List<Department> data = _db.getData() as List<Department>;
            if (data is null)
            {
                return NotFound();
            }
            return Ok(_db.getData());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            _deptreqlog.logRequest("GET SINGLE");
            Department data = _db.getData(id);
            if (data is null)
            {
                return StatusCode(404);
            }
            return Ok(_db.getData(id));
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] Department department)
        {
            _deptreqlog.logRequest("POST");
            _db.postData(department);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Department department)
        {
            _deptreqlog.logRequest("PUT");
            _db.putData(id, department);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _deptreqlog.logRequest("DELETE");
            _db.delData(id);
        }
    }
}
