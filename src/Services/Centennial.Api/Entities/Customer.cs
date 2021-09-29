using System;
using System.ComponentModel.DataAnnotations;

namespace Centennial.Api.Entities
{
    public record Customer: BaseEntity<string>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string CreatedBy { get; set; }

        private Customer()
        {
            Id = Guid.NewGuid().ToString();
            CreatedDate = DateTime.UtcNow;
            IsActive = true;
        }

        public Customer(DateTime? createdDate = null)
        {
            IsActive = true;
            CreatedDate = createdDate ?? DateTime.UtcNow;
        }

        public Customer(string id, string name, string phoneNumber, string createdBy, DateTime createdDate, bool isActive)
        {
            Id = id;
            Name = name;
            PhoneNumber = phoneNumber;
            CreatedBy = createdBy;
            CreatedDate = createdDate;
            IsActive = isActive;
        }
    }
}
