using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Warehouse.Application.Common.Interfaces;
using Warehouse.Application.Extensions;
using Warehouse.Application.Models.Dto;

namespace Warehouse.Application.Queries.Warehouse;

public record ProductQueryHandler : IRequestHandler<ProductQuery, ProductDto>
{
    private readonly IProductService _productService;
    private readonly IValidator<ProductQuery> _productQueryValidator;
    private readonly ILogger<ProductQueryHandler> _logger;
    private readonly IMapper _mapper;

    public ProductQueryHandler(
        IProductService productService,
        IValidator<ProductQuery>
        productQueryValidator,
        ILogger<ProductQueryHandler> logger,
        IMapper mapper)
    {
        _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        _productQueryValidator = productQueryValidator ?? throw new ArgumentNullException(nameof(productQueryValidator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<ProductDto> Handle(ProductQuery request, CancellationToken cancellationToken)
    {
        LoggingExtensions.LogValidatingRequest(_logger);
        var validationResult = _productQueryValidator.Validate(request);

        if (!validationResult.IsValid)
        {
            LoggingExtensions.LogRequestValidationFailed(_logger);
            throw new ValidationException(validationResult.Errors);
        }

        var requestItems = _mapper.Map<ItemsDto>(request);
        var result =  await _productService.GetFilteredProductsAsync(requestItems);

        return result;
    }
}
