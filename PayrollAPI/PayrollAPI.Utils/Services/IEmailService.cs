﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollAPI.Utils.Services
{
    public interface IEmailService
    {
        void Send(string to, string subject, string html, string from = null);
    }
}
