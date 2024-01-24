namespace Warehouse.Domain.Entities;
public class Size
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<OrderDetails> OrderDetails { get; set; }
    public ICollection<ProductSize> ProductSizes { get; set; }

}
