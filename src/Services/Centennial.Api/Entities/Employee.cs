using System;
using System.ComponentModel.DataAnnotations;

namespace Centennial.Api.Entities
{
    public record Employee: BaseEntity<string>
    {
        [Required]
        public string Name { get; set; }
        public string CreatedBy { get; set; }

        public Employee()
        {
            CreatedDate = DateTime.UtcNow;
            IsActive = true;
            Id = Guid.NewGuid().ToString();
        }
    }
}
