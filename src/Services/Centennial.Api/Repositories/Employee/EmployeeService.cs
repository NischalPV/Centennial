using System;
using Centennial.Api.Data;
using Centennial.Api.Interfaces;
using Microsoft.Extensions.Logging;

namespace Centennial.Api.Repositories
{
    public class EmployeeService : EfRepository<Entities.Employee, string>, IEmployeeRepository
    {
        private readonly CentennialDbContext _context;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(CentennialDbContext context, ILogger<EmployeeService> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }
    }
}
