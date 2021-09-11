using System;
namespace Centennial.Api.Interfaces
{
    public interface IMaterialRepository : IAsyncRepository<Entities.Material, string>
    {
    }
}
