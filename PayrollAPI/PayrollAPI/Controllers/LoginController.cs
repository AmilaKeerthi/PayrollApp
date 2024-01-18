using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PayrollAPI.Domain.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PayrollAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO user)
        {
            if (user is null)
            {
                return BadRequest("Invalid user request!!!");
            } 
            else if(user.Email != null && user.Password != null)
            {   

                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("6AD2EFDE - AB2C - 4841 - A05E - 7045C855BA22"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    issuer: "https://localhost:7091",
                    audience: "http://localhost:4200",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(6),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new UserDTO { Token = tokenString });
            }
            return Unauthorized();
        }
    }
}
