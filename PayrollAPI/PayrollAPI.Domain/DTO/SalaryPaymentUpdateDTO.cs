﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollAPI.Domain.DTO
{
    public class SalaryPaymentUpdateDTO
    {
        public int SalaryPaymentId { get; set; }
        public DateTime PaymentDate { get; set; }
        public int EmployeeId { get; set; }
        public decimal Amount { get; set; }

    }
}
