using Warehouse.Application.Models.Dto;

namespace Warehouse.Api.Models.Requests.Basket;

public class BasketUpdateRequest
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public List<BasketLineDto>? BasketLines { get; set; }
}
