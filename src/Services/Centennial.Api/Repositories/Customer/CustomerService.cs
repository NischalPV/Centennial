using System;
using Centennial.Api.Data;
using Centennial.Api.Interfaces;
using Microsoft.Extensions.Logging;

namespace Centennial.Api.Repositories
{
    public class CustomerService: EfRepository<Entities.Customer, string>, ICustomerRepository
    {
        private readonly CentennialDbContext _context;
        private readonly ILogger<CustomerService> logger;

        public CustomerService(CentennialDbContext context, ILogger<CustomerService> logger): base(context, logger)
        {
            _context = context;
            this.logger = logger;
        }
    }
}
