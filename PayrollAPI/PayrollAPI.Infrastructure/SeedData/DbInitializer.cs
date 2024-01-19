using PayrollAPI.Domain.Models;
using PayrollAPI.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollAPI.Infrastructure.SeedData
{
    public class DbInitializer
    {
        internal static void Initialize(AppDBContext dbContext)
        {
            ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));
            dbContext.Database.EnsureCreated();
            if (dbContext.Employee.Any()) return;
            var employees = new Employee[]
            {
            new Employee
            {
                EmployeeId = 1,
                FullName = "John Doe",
                Email = "john.doe@example.com",
                Salary = 50000,
                JoinDate = new DateTime(2022, 1, 1),
                PhoneNumber = "123-456-7890",
                Address = "123 Main St",
                IsActive = true
            }, };
            foreach (var emp in employees)
                dbContext.Employee.Add(emp);
            if (dbContext.User.Any()) return;

            var users = new User[]
            {

            new User
            {
                UserId = 0,
                EmployeeId = 1,
                Email = "john.doe@example.com",
                PasswordHash = "123", // Replace with an actual hashed password
                IsAdmin = true
            },
            //add other users
            };

            foreach (var user in users)
                dbContext.User.Add(user);

            dbContext.SaveChanges();
        }
    }
}
