namespace Warehouse.Domain.Entities;

public class Group
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public ICollection<ProductGroup> ProductGroups { get; set; }
}