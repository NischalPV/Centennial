using System;
namespace Centennial.Api.Entities
{
    public record Process: BaseEntity<int>
    {
        public string Name { get; set; }
        public bool IsMandatory { get; set; }
        public bool IsRemovable { get; set; }
        public bool IsOutBound { get; set; }
        public string CreatedBy { get; set; }

        public Process()
        {
            IsActive = true;
            CreatedDate = DateTime.UtcNow;
        }
    }
}
