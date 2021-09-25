using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Centennial.Api.Interfaces
{
    public interface IProductRepository : IAsyncRepository<Entities.Product, string>
    {
        Task<Entities.Product> AddProductionProcessesAsync(List<Entities.ProductionProcess> productionProcesses, string productId);
    }
}
