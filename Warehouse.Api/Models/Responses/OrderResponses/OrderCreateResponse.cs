namespace Warehouse.Api.Models.OrderResponses.Orders;

public class OrderCreateResponse
{
    public Guid Id { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
}
