using System;
namespace Centennial.Api.Entities.Idempotency
{
    public record ClientRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Time { get; set; }
    }
}
