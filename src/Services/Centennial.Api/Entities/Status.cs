using System;
namespace Centennial.Api.Entities
{
    public record Status
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
