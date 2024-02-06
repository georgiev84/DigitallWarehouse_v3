namespace Warehouse.Api.Models.Responses;

public class CreateOrderResponse
{
    public Guid Id { get; set; }
    public string Status { get; set; }
    public DateTime OrderDate { get; set; }
    public string FullName { get; set; }
    public decimal TotalAmount { get; set; }
}
