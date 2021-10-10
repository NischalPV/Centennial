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

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(15)]
        public string GSTNumber { get; set; }

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

        public Customer(string id, string name, string phoneNumber, string gstNumber, string createdBy, DateTime createdDate, bool isActive)
        {
            Id = id;
            Name = name;
            PhoneNumber = phoneNumber;
            GSTNumber = gstNumber;
            CreatedBy = createdBy;
            CreatedDate = createdDate;
            IsActive = isActive;
        }
    }
}
