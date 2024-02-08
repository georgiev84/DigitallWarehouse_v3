namespace Warehouse.Domain.Entities;
public class Size
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<OrderLine> OrderLines { get; set; }
    public ICollection<ProductSize> ProductSizes { get; set; }
    public ICollection<BasketLine> BasketLines { get; set; }

}
