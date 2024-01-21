using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollAPI.Utils.Services
{
    public interface IUserProvider
    {
        string GetUserId();
        bool IsAdmin();

    }
}
