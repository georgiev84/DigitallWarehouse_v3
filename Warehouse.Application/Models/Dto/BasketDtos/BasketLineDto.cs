namespace Warehouse.Application.Models.Dto.BasketDtos;

public class BasketLineDto
{
    public Guid BasketId { get; set; }
    public Guid ProductId { get; set; }
    public Guid SizeId { get; set; }
    public string SizeName { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}