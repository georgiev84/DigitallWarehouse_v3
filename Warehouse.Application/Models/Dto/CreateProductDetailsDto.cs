namespace Warehouse.Application.Models.Dto;
public class CreateProductDetailsDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Brand { get; set; }
    public List<string> Groups { get; set; }
    public List<SizeDto> Sizes { get; set; }
}
