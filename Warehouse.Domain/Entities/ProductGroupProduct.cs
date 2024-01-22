namespace Warehouse.Domain.Entities;
public class ProductGroupProduct
{
    public Guid ProductGroupId { get; set; }
    public ProductGroup ProductGroup { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
}
