using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayrollAPI.Business.Core;
using PayrollAPI.Domain.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PayrollAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly IEmployeeService _EmployeeService;

        public EmployeeController(IEmployeeService EmployeeService)
        {
            _EmployeeService = EmployeeService;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployee()
        {
            return Ok(await _EmployeeService.GetAllAsync());
        }

        // GET: api/Employees
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployee(int userId)
        {
            return Ok(await _EmployeeService.GetByIdAsync(userId));
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, EmployeeDTO Employee)
        {
            if (id != Employee.EmployeeId)
            {
                return BadRequest();
            }

            try
            {
                var result = await _EmployeeService.EditAsync(Employee);

                return Ok(result);
            }
            catch (Exception)
            {

                throw;
            }

            return NoContent();
        }

        // POST: api/Employees
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<EmployeeDTO>> PostEmployee(EmployeeDTO Employee)
        {

            var result = await _EmployeeService.CreateAsync(Employee);

            return Ok(result);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<EmployeeDTO>> DeleteEmployee(int id)
        {


            return Ok(await _EmployeeService.RemoveAsync(id));
        }

        // POST: api/Employees/SalaryPayment
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("SalaryPayment")]
        public async Task<ActionResult<SalaryPaymentUpdateDTO>> SalaryPayment(SalaryPaymentUpdateDTO salary)
        {

            var result = await _EmployeeService.AddSalaryPayment(salary);

            return Ok(result);
        }
    }
}