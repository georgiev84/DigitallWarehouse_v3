using Warehouse.Domain.Entities;

namespace Warehouse.Api.Models.Requests.Orders;

public class OrderLineRequest
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public Guid SizeId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
