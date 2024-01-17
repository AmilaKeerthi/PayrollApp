using Microsoft.EntityFrameworkCore;
using PayrollAPI.Domain.Models;

namespace PayrollAPI.Infrastructure.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }
        public DbSet<User> User { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<SalaryPayment> SalaryPayment { get; set; }
    }
}
