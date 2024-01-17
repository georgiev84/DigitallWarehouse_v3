using MediatR;
using Warehouse.Application.Models.Dto;

namespace Warehouse.Application.Queries.Warehouse;

public record ProductQuery : IRequest<ProductResponseDto>
{
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public string? Highlight { get; set; }
    public string? Size { get; set; }
}
