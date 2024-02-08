namespace Warehouse.Api.Models.Responses.BasketResponses;

public class BasketResponse
{
    public Guid Id { get; set; }
    public string? Status { get; set; }
    public DateTime OrderDate { get; set; }
    public string? FullName { get; set; }
    public decimal TotalAmount { get; set; }
    public List<BasketLineResponse>? BasketLines { get; set; }
}
