using MediatR;
using Warehouse.Application.Models.Dto;

namespace Warehouse.Application.Features.Queries.Product;

public record ProductQuery(
    decimal? MinPrice, 
    decimal? MaxPrice, 
    string? Highlight, 
    string? Size) : IRequest<ProductDto>;
