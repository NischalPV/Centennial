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
    public class ProductService : EfRepository<Entities.Product, string>, IProductRepository
    {
        private readonly CentennialDbContext _context;
        private readonly ILogger<ProductService> _logger;

        public ProductService(CentennialDbContext context, ILogger<ProductService> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public override async Task<List<Entities.Product>> ListAllAsync()
        {
            return await _context.Products
                .Include(m => m.Material)
                .Where(p => p.IsActive).ToListAsync();
        }

        public override async Task<Entities.Product> GetByIdAsync(string id)
        {
            return await _context.Products
                .Include(m => m.Material)
                .Where(p => p.IsActive && p.Id == id).SingleOrDefaultAsync();
        }

        public override async Task<Entities.Product> AddAsync(Entities.Product product, bool doSave = true)
        {
            var productPriceRecord = new Entities.ProductPriceRecord()
            {
                ProductId = product.Id,
                Price = product.Price
            };
            var Material = await _context.Materials.FindAsync(product.MaterialId);
            product.AddProductPriceRecord(product.Id, product.Price, 0);
            product.SetUniqueIdentifier(product.Name, product.Dimensions, product.Price, Material.Name);

            _context.Set<Entities.Product>().Add(product);


            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occured while adding Product");
            }

            return product;
        }

        public override async Task<Entities.Product> UpdateAsync(Entities.Product product, bool doSave = true)
        {
            var lastPriceRecord = await _context.ProductPriceRecords.Where(p => p.ProductId == product.Id).OrderByDescending(p => p.CreatedDate).FirstOrDefaultAsync();

            if(lastPriceRecord.Price != product.Price)
            {
                product.AddProductPriceRecord(product.Id, product.Price, (lastPriceRecord.Price - product.Price));
            }

            if(product.Material != null)
            {
                product.SetUniqueIdentifier(product.Name, product.Dimensions, product.Price, product.Material.Name);

            }
            else
            {
                var Material = await _context.Materials.FindAsync(product.MaterialId);
                product.SetUniqueIdentifier(product.Name, product.Dimensions, product.Price, Material.Name);
            }


            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occured while saving Product");
            }

            return product;
        }

        public override async Task DeleteAsync(Entities.Product product, bool doSave = true)
        {
            product.IsActive = false;

            var existingPriceRecords = await _context.ProductPriceRecords.Where(p => p.ProductId == product.Id).ToListAsync();

            existingPriceRecords.ForEach(x => x.IsActive = false);

            _context.ProductPriceRecords.UpdateRange(existingPriceRecords);
            _context.Products.Update(product);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occured while deleting Product");
            }
        }
    }
}
