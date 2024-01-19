using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollAPI.Utils.Settings
{
    public class AuthSettings
    {
        public string Secrect { get; set; } 
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
