using System;
namespace Centennial.Api.Interfaces
{
    public interface IRawMaterialRepository : IAsyncRepository<Entities.RawMaterial, string>
    {
    }
}
