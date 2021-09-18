using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Centennial.Api.Data;
using Centennial.Api.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public override async Task<List<Entities.RawMaterial>> ListAllAsync()
        {
            return await _context.RawMaterials
                .Include(m => m.Material)
                .Where(p => p.IsActive).ToListAsync();
        }

        public override async Task<Entities.RawMaterial> GetByIdAsync(string id)
        {
            return await _context.RawMaterials
                .Include(m => m.Material)
                .Where(p => p.IsActive && p.Id == id).SingleOrDefaultAsync();
        }


    }
}
