using System;
namespace Centennial.Api.Entities
{
    public record TransactionType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
