using Warehouse.Domain.Entities;

namespace Warehouse.Api.Models.Responses;

public class OrderLineResponse
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public Guid SizeId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string? Product { get; set; }

    public string? Size { get; set; }
}
