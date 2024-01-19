using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PayrollAPI.Business.Core;
using PayrollAPI.Domain.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PayrollAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _UserService;

        public LoginController(IUserService UserService)
        {
            _UserService = UserService;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO user)
        {
            if (user is null)
            {
                return BadRequest("Invalid user request!!!");
            } 
            else if(user.Email != null && user.Password != null)
            {

                var result = await _UserService.Login(user);
                if(result != null)
                {
                    return Ok(result);
                }
                
            }
            return Unauthorized();
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword ([FromBody] ChangePasswordDTO pwdChange)
        {
            if (pwdChange is null)
            {
                return BadRequest("Invalid user request!!!");
            }
            else if (pwdChange.Email != null && pwdChange.OldPassword != null && pwdChange.NewPassword != null)
            {

                var result = await _UserService.ChangePassword(pwdChange);
                if (result)
                {
                    return Ok(result);
                } else
                {
                    return BadRequest("Password change request falied.");
                }

            }
            return Unauthorized();
        }
    }
}
