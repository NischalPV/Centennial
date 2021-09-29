using System;
using System.ComponentModel.DataAnnotations;

namespace Centennial.Api.Entities
{
    public record Material: BaseEntity<string>
    {
        [Required]
        public string Name { get; set; }
        public string CreatedBy { get; set; }

        public Material()
        {
            Id = Guid.NewGuid().ToString();
            CreatedDate = DateTime.UtcNow;
            IsActive = true;
        }

        public Material(string name)
        {
            Name = name;
            Id = Guid.NewGuid().ToString();
            CreatedDate = DateTime.UtcNow;
            IsActive = true;
        }

        public Material(string id, string name, DateTime createdDate)
        {
            Name = name;
            Id = id;
            CreatedDate = createdDate;
            IsActive = true;
        }

    }
}
