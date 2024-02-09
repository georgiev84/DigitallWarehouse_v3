namespace Warehouse.Api.Models.Requests.BasketLine;

public class BasketLineUpdateRequest
{
    public Guid BasketLineId { get; set; }
    public Guid SizeId { get; set; }
    public int Quantity { get; set; }
}
