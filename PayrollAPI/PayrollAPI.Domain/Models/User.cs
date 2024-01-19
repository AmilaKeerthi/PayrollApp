﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollAPI.Domain.Models
{
    public class User
    {
        public int UserId { get; set; }
        public int EmployeeId { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool IsAdmin { get; set; }

        public virtual Employee Employee { get; set; }

    }
}
