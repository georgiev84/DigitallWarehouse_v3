using System.ComponentModel.DataAnnotations;

namespace Warehouse.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public ICollection<string> Sizes { get; set; }
        public string Description { get; set; }
        public Guid ProductGroupId { get; set; }
        public ICollection<ProductGroup> ProductGroups { get; set; }
    }
}
