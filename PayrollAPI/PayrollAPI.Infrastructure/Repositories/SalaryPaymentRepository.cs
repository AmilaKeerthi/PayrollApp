using PayrollAPI.Domain.Interfaces;
using PayrollAPI.Domain.Models;
using PayrollAPI.Infrastructure.Data;


namespace PayrollAPI.Infrastructure.Repositories
{
    public class SalaryPaymentRepository : GenericRepository<SalaryPayment>, ISalaryPaymentRepository
    {
        public SalaryPaymentRepository(AppDBContext context) : base(context)
        {
        }

        public AppDBContext AppDbContext => context as AppDBContext;

    }
}
