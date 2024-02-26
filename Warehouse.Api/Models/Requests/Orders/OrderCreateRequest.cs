namespace Warehouse.Api.Models.Requests.Orders;

public class OrderCreateRequest
{
    public Guid StatusId { get; set; }
    public Guid PaymentId { get; set; }
    public DateTime OrderDate { get; set; }
    public Guid UserId { get; set; }
    public decimal TotalAmount { get; set; }
    public List<OrderLineRequest>? OrderLines { get; set; }
}