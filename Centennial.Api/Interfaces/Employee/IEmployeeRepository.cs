using System;
namespace Centennial.Api.Interfaces
{
    public interface IEmployeeRepository : IAsyncRepository<Entities.Employee, string>
    {
    }
}
