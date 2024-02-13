namespace Warehouse.Domain.Entities;

public class OrderStatus
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public List<Order> Orders { get; set; }
}