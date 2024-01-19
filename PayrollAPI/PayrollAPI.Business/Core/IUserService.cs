using PayrollAPI.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollAPI.Business.Core
{
    public interface IUserService
    {
        Task<UserDTO> Login(LoginDTO login);
        Task<bool> ChangePassword(ChangePasswordDTO login);

    }
}
