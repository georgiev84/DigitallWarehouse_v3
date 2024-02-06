namespace Warehouse.Domain.Entities;

public class Product
{
    public Guid Id { get; set; }

    public Guid SizeId { get; set; }

    public Guid BrandId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public bool IsDeleted { get; set; }

    public ICollection<ProductGroup> ProductGroups { get; set; }
    public ICollection<OrderLine> OrderLines { get; set; }
    public ICollection<ProductSize> ProductSizes { get; set; }
    public Brand Brand { get; set; }
}
