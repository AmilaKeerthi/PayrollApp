using AutoMapper;
using PayrollAPI.Business.Core;
using PayrollAPI.Domain.DTO;
using PayrollAPI.Domain.Interfaces;
using PayrollAPI.Domain.Models;

namespace PayrollAPI.Business.Services
{
    public class SalaryPaymentService : ISalaryPaymentService
    {
        private readonly ISalaryPaymentRepository _SalaryPaymentRepository;
        private readonly IEmployeeRepository _EmployeeRepository;
        private readonly IMapper mapper;

        public SalaryPaymentService(
            ISalaryPaymentRepository SalaryPaymentRepository,
            IEmployeeRepository EmployeeRepository,
            IMapper mapper
            )
        {
            _SalaryPaymentRepository = SalaryPaymentRepository;
            this.mapper = mapper;

        }


        public async Task<SalaryPaymentDTO> CreateAsync(SalaryPaymentUpdateDTO SalaryPayment)
        {
            try
            {

                var Employee = await _EmployeeRepository.FindAsync(e => e.EmployeeId.Equals(SalaryPayment.EmployeeId));
                if(Employee != null)
                {
                    var updatedDetails = new SalaryPayment()
                    {
                        Amount = SalaryPayment.Amount,
                        EmployeeId = SalaryPayment.EmployeeId,
                        PaymentDate = SalaryPayment.PaymentDate,
                        SalaryPaymentId = 0,
                        Employee = Employee
                    };
                    var result = await _SalaryPaymentRepository.AddAsync(updatedDetails);
                    await _SalaryPaymentRepository.CompleteAsync();
                    return mapper.Map<SalaryPayment, SalaryPaymentDTO>(result);
                } else
                {
                    return null;
                }
                

              
            }
            catch (Exception e)
            {
                throw;
            }
        }


        public async Task<SalaryPaymentDTO> EditAsync(SalaryPaymentUpdateDTO SalaryPayment)
        {
            try
            {
                var Employee = await _EmployeeRepository.FindAsync(e => e.EmployeeId.Equals(SalaryPayment.EmployeeId));
                if (Employee != null)
                {
                    var updatedDetails = new SalaryPayment()
                    {
                        Amount = SalaryPayment.Amount,
                        EmployeeId = SalaryPayment.EmployeeId,
                        PaymentDate = SalaryPayment.PaymentDate,
                        SalaryPaymentId = SalaryPayment.SalaryPaymentId,
                        Employee = Employee
                    };
                    var result = await _SalaryPaymentRepository.UpdateAsync(updatedDetails, SalaryPayment.SalaryPaymentId);
                    await _SalaryPaymentRepository.CompleteAsync();
                    return mapper.Map<SalaryPayment, SalaryPaymentDTO>(result);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<IEnumerable<SalaryPaymentDTO>> GetAllAsync()
        {
            try
            {
                IEnumerable<SalaryPayment> SalaryPayment = await _SalaryPaymentRepository.GetAllIncludingAsync(salary=>salary.Employee);
                return mapper.Map<IEnumerable<SalaryPayment>, IEnumerable<SalaryPaymentDTO>>(SalaryPayment);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<SalaryPaymentDTO> GetByIdAsync(int id)
        {
            try
            {
                var SalaryPayment = await _SalaryPaymentRepository.FindAsync(ent => ent.SalaryPaymentId == id);
                return mapper.Map<SalaryPayment, SalaryPaymentDTO>(SalaryPayment);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<bool> RemoveAsync(int id)
        {
            try
            {
                var SalaryPayment = await _SalaryPaymentRepository.FindAsync(ent => ent.SalaryPaymentId == id);
                _SalaryPaymentRepository.Delete(SalaryPayment);
                await _SalaryPaymentRepository.CompleteAsync();

                return true;

            }
            catch (Exception e)
            {
                return false;
            }
        }




        public async Task<IEnumerable<SalaryPaymentDTO>> SearchAsync(string hint)
        {
            try
            {
                if (hint == null || hint == "")
                {
                    IEnumerable<SalaryPayment> SalaryPayment = await _SalaryPaymentRepository.GetAllAsync();
                    return mapper.Map<IEnumerable<SalaryPayment>, IEnumerable<SalaryPaymentDTO>>(SalaryPayment);

                }
                else
                {
                    var User = await _SalaryPaymentRepository.FindAllAsync(ent => ent.Amount.Equals(hint));
                    return mapper.Map<IEnumerable<SalaryPayment>, IEnumerable<SalaryPaymentDTO>>(User);
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}