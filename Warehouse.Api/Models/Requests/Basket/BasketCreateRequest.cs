using Warehouse.Application.Models.Dto;

namespace Warehouse.Api.Models.Requests.Basket;

public class BasketCreateRequest
{
    public Guid UserId { get; set; }
    public List<BasketLineDto>? BasketLines { get; set; }
}
