using Warehouse.Domain.Entities;

namespace Warehouse.Domain.Models;
public class ProductDetailsDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Brand { get; set; }
    public List<string> Groups { get; set; }
    public List<SizeDto> Sizes { get; set; }
}
