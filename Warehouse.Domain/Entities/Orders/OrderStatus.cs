using Warehouse.Domain.Enums;

namespace Warehouse.Domain.Entities.Orders;

public class OrderStatus
{
    public Guid Id { get; set; }
    public OrderStatusName Name { get; set; }
    public List<Order> Orders { get; set; }
}