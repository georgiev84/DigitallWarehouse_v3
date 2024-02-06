using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Application.Extensions;
using Warehouse.Application.Models.Dto;
using Warehouse.Domain.Exceptions;
using Warehouse.Infrastructure.Extensions;

namespace Warehouse.Application.Features.Queries.Product.ProductList;

public record ProductListQueryHandler : IRequestHandler<ProductListQuery, ProductDto>
{
    private readonly ILogger<ProductListQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public ProductListQueryHandler(
        ILogger<ProductListQueryHandler> logger,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _unitOfWork = unitOfWork;
    }

    public async Task<ProductDto> Handle(ProductListQuery request, CancellationToken cancellationToken)
    {
        _logger.LogRequestHandlerMessage(request);
        var requestItems = _mapper.Map<ItemsDto>(request);

        _logger.LogGettingProducts();

        var rawProducts = await _unitOfWork.Products.GetProductsDetailsAsync();

        var allProducts = _mapper.Map<IEnumerable<ProductDetailsDto>>(rawProducts);

        if (allProducts == null)
        {
            _logger.LogErrorFetchingProducts();
            throw new ProductNotFoundException("No products found in the database");
        }

        // Extract min and max prices
        decimal? overallMinPrice = allProducts.Min(p => p.Price);
        decimal? overallMaxPrice = allProducts.Max(p => p.Price);

        var sizeNames = await _unitOfWork.Sizes.GetSizeNamesAsync();

        // Extract and split descriptions
        var wordOccurrences = allProducts.GetWordOccurrences();

        // Exctract common words
        var excludedWords = wordOccurrences.Take(5).ToList();
        var commonWords = wordOccurrences.Skip(5).Take(10).Except(excludedWords).ToArray();

        // Filter products
        _logger.LogFilteringProducts();
        var filteredProducts = allProducts;

        filteredProducts = filteredProducts
            .FilterByMinPrice(requestItems.MinPrice)
            .FilterByMaxPrice(requestItems.MaxPrice)
            .FilterBySize(requestItems.Size)
            .HighlightWords(requestItems.Highlight);

        return new ProductDto
        {
            Filter = new ProductFilter
            {
                MinPrice = overallMinPrice,
                MaxPrice = overallMaxPrice,
                AllSizes = sizeNames,
                CommonWords = commonWords
            },
            Products = filteredProducts,
        };
    }
}
