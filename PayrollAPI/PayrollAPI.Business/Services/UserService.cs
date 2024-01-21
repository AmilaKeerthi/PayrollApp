using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PayrollAPI.Business.Core;
using PayrollAPI.Domain.DTO;
using PayrollAPI.Domain.Interfaces;
using PayrollAPI.Domain.Models;
using PayrollAPI.Utils.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PayrollAPI.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _UserRepository;
        private readonly AuthSettings _appSettings;


        public UserService(IUserRepository UserRepository, IOptions<AuthSettings> appSettings)
        {
            _UserRepository = UserRepository;
            _appSettings = appSettings.Value;
        }

        public async Task<bool> ChangePassword(ChangePasswordDTO login)
        {
            // Hash password during registration
            var passwordHash = login.OldPassword;//new PasswordHasher<LoginDTO>().HashPassword(login, login.Password);
            var user = await _UserRepository.FindIncludingAsync(u => u.Email == login.Email, inc => inc.Employee);
            // Store the hashed password in your database
            var storedHashedPassword = user.PasswordHash;
            // Verify password during login
            // var passwordVerificationResult = new PasswordHasher<LoginDTO>().VerifyHashedPassword(login, storedHashedPassword, passwordHash);

            if (login.OldPassword == storedHashedPassword)//passwordVerificationResult == PasswordVerificationResult.Success)
            {
                user.PasswordHash = login.NewPassword;
                user.IsAdmin = true;
                var userResult = await _UserRepository.UpdateAsync(user, user.UserId);

                await _UserRepository.CompleteAsync();

                return true;
            }
            else
            {
                // Passwords do not match
                return false;
            }

        }

        public async Task<UserDTO> Login(LoginDTO login)
        {
            // Hash password during registration
            var passwordHash = login.Password;//new PasswordHasher<LoginDTO>().HashPassword(login, login.Password);
            var user = await _UserRepository.FindIncludingAsync(u => u.Email == login.Email, inc => inc.Employee);
            // Store the hashed password in your database
            var storedHashedPassword = user.PasswordHash;
            // Verify password during login
            // var passwordVerificationResult = new PasswordHasher<LoginDTO>().VerifyHashedPassword(login, storedHashedPassword, passwordHash);

            if (user.Employee.IsActive && login.Password == storedHashedPassword)//passwordVerificationResult == PasswordVerificationResult.Success)
            {

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim("UserId", user.UserId.ToString()),
                    new Claim(ClaimTypes.Role, user.IsAdmin?"Admin":"User"),
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Secrect));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _appSettings.Issuer,
                    audience: _appSettings.Audience,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30), // Set expiration as needed
                    signingCredentials: creds
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);


                return new UserDTO()
                {
                    UserId = user.UserId,
                    Email = login.Email,
                    Token = tokenString,
                    IsAdmin = user.IsAdmin,
                    FullName = user.Employee.FullName,
                    EmpId = user.EmployeeId
                };
            }
            else
            {
                // Passwords do not match
                return null;
            }



        }
    }
}
