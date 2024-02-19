using Warehouse.Domain.Entities.Products;

namespace Warehouse.Domain.Entities.Baskets;

public class BasketLine
{
    public Guid Id { get; set; }
    public Guid BasketId { get; set; }
    public Guid ProductId { get; set; }
    public Guid SizeId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public Basket? Basket { get; set; }
    public Product? Product { get; set; }
    public Size? Size { get; set; }
}