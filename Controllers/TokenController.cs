using APIGenerator.Data;
using APIGenerator.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace APIGenerator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly MyDBContext _context;
        public TokenController(IConfiguration configuration, MyDBContext context) {
            _configuration = configuration;
            _context = context;
        }

        [HttpPost]
        //[Route("GetToken")]
        public IActionResult Authentication([FromBody] UsuariosDTO user) 
        {
            if (IsValidUser(user))
            {
                var token = GenerateToken();

                return Ok(new { token });
            }
            return NotFound();
        }

        private bool IsValidUser(UsuariosDTO login)
        {
            var user = _context.Usuarios.Where(a => a.UserName == login.UserName && a.Pwd == login.Pass).FirstOrDefault();

            return user != null ? true : false;
        }

        private string GenerateToken()
        {
            var symetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]));
            var signingCredentials = new SigningCredentials(symetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, "Darius"),
                new Claim(ClaimTypes.Name, "dario.almeida@globant.com"),
                new Claim(ClaimTypes.Role, "Administrador")
            };

            var payload = new JwtPayload
            (
                  _configuration["Authentication:Issuer"],
                  _configuration["Authentication:Audience"],
                  claims,
                  DateTime.Now,
                  DateTime.UtcNow.AddMinutes(5)
            );

            var token = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(token) ;
        }
    }
}
