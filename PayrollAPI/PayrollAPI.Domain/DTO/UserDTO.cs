using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollAPI.Domain.DTO
{
    public class UserDTO
    {
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public string? Token { get; set; }
        public string? FullName { get; set; }
        public int UserId { get; set; }
        public int EmpId { get; set; }

    }
}
