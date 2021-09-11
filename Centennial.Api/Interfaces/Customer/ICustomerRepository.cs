using System;
namespace Centennial.Api.Interfaces
{
    public interface ICustomerRepository : IAsyncRepository<Entities.Customer, string>
    {
    }
}
