namespace Warehouse.Api.Models.Responses.BasketResponses;

public class BasketLineUpdateResponse
{
    public Guid BasketLineId { get; set; }
    public Guid SizeId { get; set; }
    public int Quantity { get; set; }
}