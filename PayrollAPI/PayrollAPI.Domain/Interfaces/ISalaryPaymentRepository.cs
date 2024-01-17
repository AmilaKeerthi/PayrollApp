using PayrollAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollAPI.Domain.Interfaces
{
    public interface ISalaryPaymentRepository : IGenericRepository<SalaryPayment>
    {
    }
}
