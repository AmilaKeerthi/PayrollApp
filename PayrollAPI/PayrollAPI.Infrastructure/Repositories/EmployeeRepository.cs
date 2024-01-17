﻿using PayrollAPI.Domain.Interfaces;
using PayrollAPI.Domain.Models;
using PayrollAPI.Infrastructure.Data;


namespace PayrollAPI.Infrastructure.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppDBContext context) : base(context)
        {
        }

        public AppDBContext AppDbContext => context as AppDBContext;

    }
}
