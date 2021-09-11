using System;
namespace Centennial.Api.Interfaces
{
    public interface IStockRecordRepository : IAsyncRepository<Entities.StockRecord, string>
    {
    }
}
