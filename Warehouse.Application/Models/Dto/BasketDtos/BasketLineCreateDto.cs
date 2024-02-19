namespace Warehouse.Application.Models.Dto.BasketDtos;

public class BasketLineCreateDto
{
    public Guid ProductId { get; set; }
    public Guid SizeId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}