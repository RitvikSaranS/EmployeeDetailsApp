using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EmployeeDetailsAPI.Interfaces;
using EmployeeDetailsAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeDetailsAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IRegAndLogin _db;
        public AuthController(IRegAndLogin db)
        {
            _db = db;
        }
        [HttpPost, Route("login")]
        public IActionResult LoginUser([FromBody] Login user)
        {
            if(user == null)
            {
                return BadRequest("Invalid Request");
            }
            if(_db.GetHash(user.username) == user.passwordHash)
            {
                var tmp = _db.GetHash(user.username);
                var secretKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("supersecretkeyformmyapp5523"));
                var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokenOptions = new JwtSecurityToken(
                    issuer: "https://localhost:44382",
                    audience: "https://localhost:4200",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signingCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(new { Token = tokenString });
            }
            return Unauthorized();
        }
    }
}