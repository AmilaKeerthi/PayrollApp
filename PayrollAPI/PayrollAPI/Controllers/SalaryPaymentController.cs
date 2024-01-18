using Microsoft.AspNetCore.Mvc;
using PayrollAPI.Business.Core;
using PayrollAPI.Domain.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PayrollAPI.Controllers
{
   
        [Route("api/[controller]")]
        [ApiController]
        public class SalaryPaymentsController : ControllerBase
        {
            private readonly ISalaryPaymentService _SalaryPaymentService;

            public SalaryPaymentsController(ISalaryPaymentService SalaryPaymentService)
            {
                _SalaryPaymentService = SalaryPaymentService;
            }

            // GET: api/SalaryPayments
            [HttpGet]
            public async Task<ActionResult<IEnumerable<SalaryPaymentDTO>>> GetSalaryPayment()
            {
                return Ok(await _SalaryPaymentService.GetAllAsync());
            }


        // GET: api/SalaryPayments/1
        [HttpGet("{userId}")]
            public async Task<ActionResult<IEnumerable<SalaryPaymentDTO>>> GetSalaryPayment(int userId)
            {
                return Ok(await _SalaryPaymentService.GetByIdAsync(userId));
            }

            // PUT: api/SalaryPayments/5
            // To protect from overposting attacks, enable the specific properties you want to bind to, for
            // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
            [HttpPut("{id}")]
            public async Task<IActionResult> PutSalaryPayment(int id, SalaryPaymentUpdateDTO SalaryPayment)
            {
                if (id != SalaryPayment.SalaryPaymentId)
                {
                    return BadRequest();
                }

                try
                {
                    var result = await _SalaryPaymentService.EditAsync(SalaryPayment);

                    return Ok(result);
                }
                catch (Exception)
                {

                    throw;
                }

                return NoContent();
            }

            // POST: api/SalaryPayments
            // To protect from overposting attacks, enable the specific properties you want to bind to, for
            // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
            [HttpPost]
            public async Task<ActionResult<SalaryPaymentDTO>> PostSalaryPayment(SalaryPaymentUpdateDTO SalaryPayment)
            {

                var result = await _SalaryPaymentService.CreateAsync(SalaryPayment);

                return Ok(result);
            }

            // DELETE: api/SalaryPayments/5
            [HttpDelete("{id}")]
            public async Task<ActionResult<SalaryPaymentDTO>> DeleteSalaryPayment(int id)
            {


                return Ok(await _SalaryPaymentService.RemoveAsync(id));
            }

        }
    }