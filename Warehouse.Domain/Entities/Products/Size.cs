using Warehouse.Domain.Entities.Baskets;
using Warehouse.Domain.Entities.Orders;

namespace Warehouse.Domain.Entities.Products;

public class Size
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<OrderLine> OrderLines { get; set; }
    public ICollection<ProductSize> ProductSizes { get; set; }
    public ICollection<BasketLine> BasketLines { get; set; }
}