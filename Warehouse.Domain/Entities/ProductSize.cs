namespace Warehouse.Domain.Entities;

public class ProductSize
{
    public Guid ProductId { get; set; }
    public Guid SizeId { get; set; }
    public int QuantityInStock { get; set; }

    public Product Product { get; set; }
    public Size Size { get; set; }
}