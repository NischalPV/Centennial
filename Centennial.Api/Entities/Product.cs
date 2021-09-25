using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;

namespace Centennial.Api.Entities
{
    public record Product: BaseEntity<string>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Dimensions { get; set; }

        [Required]
        public float Price { get; set; }

        public string UniqueIdentifier { get; protected set; }

        [Required]
        [ForeignKey(name: nameof(Material))]
        public string MaterialId { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }

        private readonly List<ProductPriceRecord> _productPriceRecords;
        private readonly List<ProductionProcess> _productionProcesses;

        public virtual Material Material { get; set; }

        public virtual IReadOnlyCollection<ProductPriceRecord> ProductPriceRecords => _productPriceRecords;
        public virtual IReadOnlyCollection<StockRecord> StockRecords { get; }
        public virtual IReadOnlyCollection<Inventory> Inventories { get; }
        public virtual IReadOnlyCollection<ProductionProcess> ProductionProcesses => _productionProcesses;

        private Product()
        {
            Id = Guid.NewGuid().ToString();
            CreatedDate = DateTime.UtcNow;
            _productPriceRecords = new List<ProductPriceRecord>();
            _productionProcesses = new List<ProductionProcess>();
            IsActive = true;
        }

        public void AddProductPriceRecord(string productId, float price, float change)
        {
            var newRecord = new ProductPriceRecord(productId, price, change);
            _productPriceRecords.Add(newRecord);
        }

        public void AddProductionProcesses(string productId, List<ProductionProcess> productionProcesses)
        {
            productionProcesses.ForEach(x => x.ProductId = productId);
            _productionProcesses.AddRange(productionProcesses);
        }

        public void SetUniqueIdentifier(string name, string dimensions, float price, string material)
        {
            UniqueIdentifier = $"{name}--{dimensions}--{material}--{price}";
        }
    }

    public record ProductPriceRecord: BaseEntity<int>
    {
        [ForeignKey(nameof(Product))]
        public string ProductId { get; set; }

        public float Price { get; set; }

        public float Change { get; set; }

        [JsonIgnore]
        public virtual Product Product { get; set; }

        public ProductPriceRecord()
        {
            IsActive = true;
            CreatedDate = DateTime.UtcNow;
        }

        public ProductPriceRecord(string productId, float price, float change)
        {
            CreatedDate = DateTime.UtcNow;
            IsActive = true;
            ProductId = productId;
            Price = price;
            Change = change;
        }

    }
}
