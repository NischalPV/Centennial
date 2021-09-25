using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Centennial.Api.Entities
{
    public record ProductionProcess : BaseEntity<string>
    {
        [ForeignKey(name: nameof(Product))]
        public string ProductId { get; set; }

        [ForeignKey(name: nameof(Process))]
        public int ProcessId { get; set; }

        public int Sequence { get; set; }
        public bool IsMandatory { get; set; }

        [JsonIgnore]
        public virtual Process Process { get; set; }

        [JsonIgnore]
        public virtual Product Product { get; set; }

        public ProductionProcess()
        {
            Id = Guid.NewGuid().ToString();
            CreatedDate = DateTime.UtcNow;
            IsActive = true;
        }
    }
}
