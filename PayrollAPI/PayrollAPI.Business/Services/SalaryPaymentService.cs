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
        private readonly IMapper mapper;

        public SalaryPaymentService(
            ISalaryPaymentRepository SalaryPaymentRepository,
            IMapper mapper
            )
        {
            _SalaryPaymentRepository = SalaryPaymentRepository;
            this.mapper = mapper;

        }


        public async Task<SalaryPaymentDTO> CreateAsync(SalaryPaymentDTO SalaryPayment)
        {
            try
            {
                var updatedDetails = mapper.Map<SalaryPaymentDTO, SalaryPayment>(SalaryPayment);
                var result = await _SalaryPaymentRepository.AddAsync(updatedDetails);
                await _SalaryPaymentRepository.CompleteAsync();
                return mapper.Map<SalaryPayment, SalaryPaymentDTO>(result);
            }
            catch (Exception e)
            {
                throw;
            }
        }


        public async Task<SalaryPaymentDTO> EditAsync(SalaryPaymentDTO SalaryPayment)
        {
            try
            {
                var updatedDetails = mapper.Map<SalaryPaymentDTO, SalaryPayment>(SalaryPayment);

                var result = await _SalaryPaymentRepository.UpdateAsync(updatedDetails, updatedDetails.EmployeeId);
                await _SalaryPaymentRepository.CompleteAsync();

                return mapper.Map<SalaryPayment, SalaryPaymentDTO>(result);
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
                IEnumerable<SalaryPayment> SalaryPayment = await _SalaryPaymentRepository.GetAllAsync();
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