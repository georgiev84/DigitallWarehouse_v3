using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using Warehouse.Application.Common.Interfaces;
using Warehouse.Domain.Responses;

namespace Warehouse.Application.Queries.Warehouse;

public record ProductQueryHandler : IRequestHandler<ProductQuery, ProductResponse>
{
    private readonly IProductService _productService;
    private readonly IValidator<ProductQuery> _productQueryValidator;

    public ProductQueryHandler(IProductService productService, IValidator<ProductQuery> productQueryValidator)
    {
        _productService = productService;
        _productQueryValidator = productQueryValidator;
    }

    public async Task<ProductResponse> Handle(ProductQuery request, CancellationToken cancellationToken)
    {
        ValidationResult validationResult = _productQueryValidator.Validate(request);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        return await _productService.GetFilteredProductsAsync(request.MinPrice, request.MaxPrice, request.Size, request.Highlight); 
    }

}
