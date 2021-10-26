using APIGenerator.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGenerator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        [HttpPost]
        public IActionResult Authentication(UserLogin login) 
        {
            if (IsValidUser(login))
            {
                var token = GenerateToken();

                return Ok(new { token });
            }
            return NotFound();
        }

        private bool IsValidUser(UserLogin login)
        {


            return true;
        }

        private string GenerateToken()
        {
            //Header
            //var symetricSecurityKey = 

            return string.Empty;
        }
    }
}
