using System;
using Centennial.Api.Data;
using Centennial.Api.Interfaces;
using Microsoft.Extensions.Logging;

namespace Centennial.Api.Repositories
{
    public class MaterialService : EfRepository<Entities.Material, string>, IMaterialRepository
    {
        private readonly CentennialDbContext _context;
        private readonly ILogger<MaterialService> logger;

        public MaterialService(CentennialDbContext context, ILogger<MaterialService> logger) : base(context, logger)
        {
            _context = context;
            this.logger = logger;
        }
    }
}
