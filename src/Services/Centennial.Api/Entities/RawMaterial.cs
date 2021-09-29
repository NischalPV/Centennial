using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Centennial.Api.Entities
{
    public record RawMaterial: BaseEntity<string>
    {
        public string Name { get; set; }
        public string Size { get; set; }
        public string CreatedBy { get; set; }

        [ForeignKey(name: nameof(Material))]
        public string MaterialId { get; set; }

        public virtual Material Material { get; set; }

        public RawMaterial()
        {
            CreatedDate = DateTime.UtcNow;
            IsActive = true;
            Id = Guid.NewGuid().ToString();
        }
    }
}
