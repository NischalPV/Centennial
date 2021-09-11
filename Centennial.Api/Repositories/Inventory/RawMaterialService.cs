using System;
using Centennial.Api.Data;
using Centennial.Api.Interfaces;
using Microsoft.Extensions.Logging;

namespace Centennial.Api.Repositories
{
    public class RawMaterialService : EfRepository<Entities.RawMaterial, string>, IRawMaterialRepository
    {
        private readonly CentennialDbContext _context;
        private readonly ILogger<RawMaterialService> _logger;

        public RawMaterialService(CentennialDbContext context, ILogger<RawMaterialService> logger) : base(context, logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    }
}
