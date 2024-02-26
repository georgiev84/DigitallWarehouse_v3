namespace Warehouse.Domain.Entities.Products;

public class Brand
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<Product> Products { get; set; }
}