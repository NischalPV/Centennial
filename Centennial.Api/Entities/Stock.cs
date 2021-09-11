using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Centennial.Api.Entities
{
    public record StockRecord: BaseEntity<string>
    {
        [ForeignKey(nameof(Product))]
        public string ProductId { get; set; }

        [ForeignKey(nameof(Process))]
        public int ProcessId { get; set; }
        public int Quantity { get; set; }
        public string CreatedBy { get; set; }

        [ForeignKey(name: nameof(Status))]
        public int StatusId { get; set; }

        public virtual Product Product { get; set; }
        public virtual Process Process { get; set; }
        public virtual Status Status { get; set; }

        public StockRecord()
        {
            CreatedDate = DateTime.UtcNow;
            Id = Guid.NewGuid().ToString();
            IsActive = true;
            StatusId = (int)Constants.StatusIds.Created;
        }

        public StockRecord(string productId, int processId, int quantity)
        {
            ProductId = productId;
            ProcessId = processId;
            Quantity = quantity;
            CreatedDate = DateTime.UtcNow;
            Id = Guid.NewGuid().ToString();
            IsActive = true;
            StatusId = (int)Constants.StatusIds.Created;
        }

    }

    public record Inventory
    {
        public string Id { get; set; }

        [ForeignKey(nameof(Product))]
        public string ProductId { get; set; }

        [ForeignKey(nameof(Process))]
        public int ProcessId { get; set; }
        public int Quantity { get; set; }
        public DateTime UpdatedOn { get; set; }

        public virtual Product Product { get; set; }
        public virtual Process Process { get; set; }

        public virtual IReadOnlyCollection<StockTransaction> Transactions { get; set; }

        public Inventory()
        {
            Id = Guid.NewGuid().ToString();
        }

        public Inventory(string productId, int processId)
        {
            Id = Guid.NewGuid().ToString();
            ProductId = productId;
            ProcessId = processId;
        }
    }

    public record StockTransaction: BaseEntity<string>
    {
        [ForeignKey(nameof(Inventory))]
        public string InventoryId { get; set; }

        [ForeignKey(name: nameof(TransactionType))]
        public int TransactionTypeId { get; set; }

        public string TransactionDetails { get; set; }

        public float Quantity { get; set; }

        public virtual Inventory Inventory { get; set; }
        public virtual TransactionType TransactionType { get; set; }

        public StockTransaction()
        {
            Id = Guid.NewGuid().ToString();
            CreatedDate = DateTime.UtcNow;
            IsActive = true;
        }
    }
}
