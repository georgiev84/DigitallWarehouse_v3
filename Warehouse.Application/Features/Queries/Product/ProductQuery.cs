using MediatR;
using Warehouse.Application.Models.Dto;

namespace Warehouse.Application.Features.Queries.Product;

public record ProductQuery : IRequest<ProductDto>
{
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public string? Highlight { get; set; }
    public string? Size { get; set; }
}
