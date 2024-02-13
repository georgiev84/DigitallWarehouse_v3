namespace Warehouse.Domain.Entities;

public class ProductGroup
{
    public Guid ProductId { get; set; }

    public Guid GroupId { get; set; }

    public Product Product { get; set; }
    public Group Group { get; set; }
}