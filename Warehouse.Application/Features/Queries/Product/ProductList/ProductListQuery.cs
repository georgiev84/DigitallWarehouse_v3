using MediatR;
using Warehouse.Application.Models.Dto;

namespace Warehouse.Application.Features.Queries.Product.ProductList;

public record ProductListQuery(
    decimal? MinPrice,
    decimal? MaxPrice,
    string? Highlight,
    string? Size) : IRequest<ProductDto>;
