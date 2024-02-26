namespace Warehouse.Application.Models.Dto.BasketDtos;

public class BasketLineUpdateDto
{
    public Guid BasketLineId { get; set; }
    public Guid SizeId { get; set; }
    public int Quantity { get; set; }
}