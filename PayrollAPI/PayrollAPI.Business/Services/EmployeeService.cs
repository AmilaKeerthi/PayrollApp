using AutoMapper;
using PayrollAPI.Business.Core;
using PayrollAPI.Domain.DTO;
using PayrollAPI.Domain.Interfaces;
using PayrollAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollAPI.Business.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _EmployeeRepository;
        private readonly IMapper mapper;

        public EmployeeService(
            IEmployeeRepository EmployeeRepository,
            IMapper mapper
            )
        {
            _EmployeeRepository = EmployeeRepository;
            this.mapper = mapper;

        }
      

        public async Task<EmployeeDTO> CreateAsync(EmployeeDTO employee)
        {
            try
            {
                var updatedDetails = mapper.Map<EmployeeDTO, Employee>(employee);
                var result = await _EmployeeRepository.AddAsync(updatedDetails);
                await _EmployeeRepository.CompleteAsync();
                return mapper.Map<Employee, EmployeeDTO>(result);
            }
            catch (Exception e)
            {
                throw;
            }
        }


        public async Task<EmployeeDTO> EditAsync(EmployeeDTO employee)
        {
            try
            {
                var updatedDetails = mapper.Map<EmployeeDTO, Employee>(employee);

                var result = await _EmployeeRepository.UpdateAsync(updatedDetails, updatedDetails.EmployeeId);
                await _EmployeeRepository.CompleteAsync();

                return mapper.Map<Employee, EmployeeDTO>(result);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<IEnumerable<EmployeeDTO>> GetAllAsync()
        {
            try
            {
                IEnumerable<Employee> Employee = await _EmployeeRepository.GetAllAsync();
                return mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDTO>>(Employee);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<EmployeeDTO> GetByIdAsync(int id)
        {
            try
            {
                var Employee = await _EmployeeRepository.FindAsync(ent => ent.EmployeeId == id);
                return mapper.Map<Employee, EmployeeDTO>(Employee);
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
                var Employee = await _EmployeeRepository.FindAsync(ent => ent.EmployeeId == id);
                _EmployeeRepository.Delete(Employee);
                await _EmployeeRepository.CompleteAsync();

                return true;

            }
            catch (Exception e)
            {
                return false;
            }
        }
        

       

        public async Task<IEnumerable<EmployeeDTO>> SearchAsync(string hint)
        {
            try
            {
                if (hint == null || hint == "")
                {
                    IEnumerable<Employee> Employee = await _EmployeeRepository.GetAllAsync();
                    return mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDTO>>(Employee);

                }
                else
                {
                    var User = await _EmployeeRepository.FindAllAsync(ent => ent.FullName.Equals(hint));
                    return mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDTO>>(User);
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}