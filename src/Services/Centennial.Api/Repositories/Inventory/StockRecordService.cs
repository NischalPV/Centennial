using System;
using System.Linq;
using System.Threading.Tasks;
using Centennial.Api.Data;
using Centennial.Api.Entities;
using Centennial.Api.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Centennial.Api.Repositories
{
    public class StockRecordService : EfRepository<Entities.StockRecord, string>, IStockRecordRepository
    {
        private readonly CentennialDbContext _context;
        private readonly ILogger<StockRecordService> _logger;

        public StockRecordService(CentennialDbContext context, ILogger<StockRecordService> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        //public override async Task<StockRecord> AddAsync(StockRecord record)
        //{
        //    record = await base.AddAsync(record);

        //    var existingInventory = await _context.Inventories.Where(i => i.ProductId == record.ProductId && i.ProcessId == record.ProcessId).FirstOrDefaultAsync();

        //    if (existingInventory == null)
        //    {
        //        await _context.Inventories.AddAsync(new Inventory() { ProcessId = record.ProcessId, ProductId = record.ProductId, Quantity = record.Quantity, UpdatedOn = DateTime.UtcNow });
        //        await _context.SaveChangesAsync();
        //    }
        //}
    }
}
