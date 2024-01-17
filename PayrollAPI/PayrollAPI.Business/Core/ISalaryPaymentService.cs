using PayrollAPI.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollAPI.Business.Core
{
    public interface ISalaryPaymentService
    {
        Task<IEnumerable<SalaryPaymentDTO>> GetAllAsync();
        Task<SalaryPaymentDTO> GetByIdAsync(int id);
        Task<SalaryPaymentDTO> CreateAsync(SalaryPaymentDTO salaryPaymentDTO);
        Task<SalaryPaymentDTO> EditAsync(SalaryPaymentDTO salaryPaymentDTO);
        Task<Boolean> RemoveAsync(int id);
        Task<IEnumerable<SalaryPaymentDTO>> SearchAsync(string hint);
    }
}
