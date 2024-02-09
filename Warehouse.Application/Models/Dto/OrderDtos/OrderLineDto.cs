namespace Warehouse.Application.Models.Dto.OrderDtos;
public class OrderLineDto
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public Guid SizeId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string? Product { get; set; }
    public string? Size { get; set; }
}
