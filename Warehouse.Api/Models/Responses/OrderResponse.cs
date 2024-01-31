namespace Warehouse.Api.Models.Responses;

public class OrderResponse
{
    public Guid Id { get; set; }
    public string Status { get; set; }
    public DateTime OrderDate { get; set; }
    public string UserName { get; set; }
    public decimal TotalAmount { get; set; }
}
