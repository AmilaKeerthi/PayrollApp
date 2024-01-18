using AutoMapper;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using PayrollAPI.Business.Core;
using PayrollAPI.Domain.DTO;
using PayrollAPI.Domain.Interfaces;
using PayrollAPI.Domain.Models;
using PayrollAPI.Utils.Services;
using System.Security.Cryptography;


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
                        //EmployeeId = SalaryPayment.EmployeeId,
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

                byte[] salt = GenerateSalt();

                // Hash the password
                string hashedPassword = HashPassword(password, salt);

               var userResult = await _UserRepository.AddAsync(new User()
                {
                    UserId = 0,
                    Email = employee.Email,
                    EmployeeId = result.EmployeeId,
                    IsAdmin = false,
                    PasswordHash = hashedPassword
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


        private static byte[] GenerateSalt()
        {
            // Generate a random salt using a secure random number generator
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        private static string HashPassword(string password, byte[] salt)
        {
            // Use the PBKDF2 algorithm to hash the password
            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000, // Adjust the iteration count as needed
                numBytesRequested: 256 / 8 // 256 bits
            ));

            // Combine the salt and hashed password for storage
            string combinedHash = Convert.ToBase64String(salt) + ":" + hashedPassword;

            return combinedHash;
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