using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PayrollAPI.Business.Core;
using PayrollAPI.Domain.DTO;
using PayrollAPI.Domain.Interfaces;
using PayrollAPI.Domain.Models;
using PayrollAPI.Utils.Services;


namespace PayrollAPI.Business.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _EmployeeRepository;
        private readonly ISalaryPaymentRepository _SalaryPaymentRepository;
        private readonly IMapper mapper;
        private readonly IEmailService _EmailService;
        private readonly IUserRepository _UserRepository;

        public EmployeeService(
            IEmployeeRepository EmployeeRepository,
            ISalaryPaymentRepository SalaryPaymentRepository,
            IEmailService EmailService,
            IUserRepository UserRepository,
            IMapper mapper
            )
        {
            _EmployeeRepository = EmployeeRepository;
            _SalaryPaymentRepository = SalaryPaymentRepository;
            _EmailService = EmailService;
            _UserRepository = UserRepository;
            this.mapper = mapper;

        }

        public async Task<SalaryPaymentDTO> AddSalaryPayment(SalaryPaymentUpdateDTO SalaryPayment)
        {
            try
            {

                var Employee = await _EmployeeRepository.FindAsync(e => e.EmployeeId.Equals(SalaryPayment.EmployeeId));
                if (Employee != null)
                {
                    var updatedDetails = new SalaryPayment()
                    {
                        Amount = SalaryPayment.Amount,
                        PaymentDate = SalaryPayment.PaymentDate,
                        SalaryPaymentId = 0,
                        Employee = Employee
                    };
                    var result = await _SalaryPaymentRepository.AddAsync(updatedDetails);
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

        public async Task<EmployeeDTO> CreateAsync(EmployeeDTO employee)
        {
            try
            {
                var updatedDetails = mapper.Map<EmployeeDTO, Employee>(employee);
                var result = await _EmployeeRepository.AddAsync(updatedDetails);
                string subject = "Payroll App Sign In";
                string password = GenerateRandomPassword();
                string html = $"<p>Your 6-digit password is: {password}</p>";

                // Send the email
                _EmailService.Send(updatedDetails.Email, subject, html);

                
                await _EmployeeRepository.CompleteAsync();
                //var login = new LoginDTO() { Email = updatedDetails.Email, Password = password };

                // Hash password during registration
                var passwordHash = password;//new PasswordHasher<LoginDTO>().HashPassword(login, password);


                var userResult = await _UserRepository.AddAsync(new User()
                {
                    UserId = 0,
                    Email = employee.Email,
                    EmployeeId = result.EmployeeId,
                    IsAdmin = false,
                    PasswordHash = passwordHash
                }) ;

                await _UserRepository.CompleteAsync();

                return mapper.Map<Employee, EmployeeDTO>(result);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private static string GenerateRandomPassword()
        {
            // Generate a random 6-digit password
            Random random = new Random();
            int password = random.Next(100000, 999999);
            return password.ToString();
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