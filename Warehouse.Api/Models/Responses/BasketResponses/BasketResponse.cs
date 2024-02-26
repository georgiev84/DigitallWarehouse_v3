using Warehouse.Application.Models.Dto.BasketDtos;

namespace Warehouse.Api.Models.Responses.BasketResponses;

public class BasketResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string FullName { get; set; }
    public decimal TotalAmount { get; set; }
    public List<BasketLineDto> BasketLines { get; set; }
}