using MediatR;
using Warehouse.Application.Common.Interfaces;
using Warehouse.Domain.Responses;

namespace Warehouse.Application.Queries.Warehouse;

public record ProductQueryHandler : IRequestHandler<ProductQuery, ProductResponse>
{
    private readonly IProductService _productService;

    public ProductQueryHandler(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<ProductResponse> Handle(ProductQuery request, CancellationToken cancellationToken) 
        => await _productService.GetFilteredProductsAsync(request.MinPrice, request.MaxPrice, request.Size, request.Highlight);
}
