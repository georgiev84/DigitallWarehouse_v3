namespace Warehouse.Application.Models.Dto.BasketDtos;

public class BasketDetailDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string FullName { get; set; }
    public decimal TotalAmount { get; set; }
    public List<BasketLineDto> BasketLines { get; set; }
}