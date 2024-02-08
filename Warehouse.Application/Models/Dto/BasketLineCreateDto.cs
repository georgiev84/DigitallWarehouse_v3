namespace Warehouse.Application.Models.Dto;
public class BasketLineCreateDto
{
    public Guid BasketId { get; set; }
    public Guid ProductId { get; set; }
    public Guid SizeId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
