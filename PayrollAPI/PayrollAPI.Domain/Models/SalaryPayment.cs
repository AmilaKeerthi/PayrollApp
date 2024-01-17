using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollAPI.Domain.Models
{
    public class SalaryPayment
    {
        public int SalaryPaymentId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }

        // Navigation property
        public Employee Employee { get; set; }
    }
}
