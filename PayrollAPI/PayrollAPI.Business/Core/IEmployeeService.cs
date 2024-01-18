using PayrollAPI.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollAPI.Business.Core
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDTO>> GetAllAsync();
        Task<EmployeeDTO> GetByIdAsync(int id);
        Task<EmployeeDTO> CreateAsync(EmployeeDTO employee);
        Task<EmployeeDTO> EditAsync(EmployeeDTO employee);
        Task<Boolean> RemoveAsync(int id);
        Task<IEnumerable<EmployeeDTO>> SearchAsync(string hint);
        Task<SalaryPaymentDTO> AddSalaryPayment(SalaryPaymentUpdateDTO SalaryPayment);

    }
}
