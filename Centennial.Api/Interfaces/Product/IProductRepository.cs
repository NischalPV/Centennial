using System;
using System.Threading.Tasks;

namespace Centennial.Api.Interfaces
{
    public interface IProductRepository : IAsyncRepository<Entities.Product, string>
    {
    }
}
