namespace Warehouse.Api.Models;

public class ProductFilterModelDto
{
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public string? Highlight { get; set; }
    public string? Size { get; set; }
}

