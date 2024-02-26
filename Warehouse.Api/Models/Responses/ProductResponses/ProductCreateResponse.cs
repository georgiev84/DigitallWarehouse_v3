namespace Warehouse.Api.Models.Responses.ProductResponses;

public class ProductCreateResponse
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? Brand { get; set; }
    public List<string>? Groups { get; set; }
    public List<SizeResponse>? Sizes { get; set; }
}