using Warehouse.Application.Models.Dto.BasketDtos;

namespace Warehouse.Api.Models.Requests.BasketLine;

public class BasketLineCreateRequest
{
    public Guid UserId { get; set; }
    public BasketLineCreateDto? BasketLine { get; set; }
}