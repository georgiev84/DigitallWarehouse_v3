namespace Warehouse.Application.Models.Dto.BasketDtos;
public class BasketUpdateDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public List<BasketLineDto>? BasketLines { get; set; }
}
