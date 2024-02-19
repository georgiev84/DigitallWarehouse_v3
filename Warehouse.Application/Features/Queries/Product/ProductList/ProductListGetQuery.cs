using MediatR;
using Warehouse.Application.Models.Dto.ProductDtos;

namespace Warehouse.Application.Features.Queries.Product.ProductList;

public record ProductListGetQuery(
    decimal? MinPrice,
    decimal? MaxPrice,
    string? Highlight,
    string? Size) : IRequest<ProductDto>;