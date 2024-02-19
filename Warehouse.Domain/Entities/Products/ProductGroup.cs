namespace Warehouse.Domain.Entities.Products;

public class ProductGroup
{
    public Guid ProductId { get; set; }
    public Guid GroupId { get; set; }
    public Product Product { get; set; }
    public Group Group { get; set; }
}