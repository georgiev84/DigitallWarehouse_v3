using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Application.Extensions;
using Warehouse.Application.Models.Dto.ProductDtos;
using Warehouse.Domain.Exceptions;
using Warehouse.Persistence.EF.Extensions;

namespace Warehouse.Application.Features.Queries.Product.ProductList;

public record ProductListQueryHandler : IRequestHandler<ProductListGetQuery, ProductDto>
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

    public async Task<ProductDto> Handle(ProductListGetQuery request, CancellationToken cancellationToken)
    {
        var requestItems = _mapper.Map<ItemsDto>(request);

        var productDetailEntities = await _unitOfWork.Products.GetProductsDetailsAsync();

        var productDetailsDtos = _mapper.Map<IEnumerable<ProductDetailsDto>>(productDetailEntities);

        var filteredProducts = FilterProducts(productDetailsDtos, requestItems);

        var productFilter = GetProductFilter(productDetailsDtos, filteredProducts);

        return new ProductDto
        {
            Filter = productFilter,
            Products = filteredProducts,
        };
    }

    private IEnumerable<ProductDetailsDto> FilterProducts(IEnumerable<ProductDetailsDto> allProducts, ItemsDto requestItems)
    {
        return allProducts
            .FilterByMinPrice(requestItems.MinPrice)
            .FilterByMaxPrice(requestItems.MaxPrice)
            .FilterBySize(requestItems.Size)
            .HighlightWords(requestItems.Highlight);
    }

    private ProductFilter GetProductFilter(IEnumerable<ProductDetailsDto> allProducts, IEnumerable<ProductDetailsDto> filteredProducts)
    {
        // Extract min and max prices
        decimal? overallMinPrice = allProducts.Min(p => p.Price);
        decimal? overallMaxPrice = allProducts.Max(p => p.Price);

        var sizeNames = _unitOfWork.Sizes.GetSizeNamesAsync().Result;

        // Extract and split descriptions
        var wordOccurrences = allProducts.GetWordOccurrences();

        // Exctract common words
        var excludedWords = wordOccurrences.Take(5).ToList();
        var commonWords = wordOccurrences.Skip(5).Take(10).Except(excludedWords).ToArray();

        return new ProductFilter
        {
            MinPrice = overallMinPrice,
            MaxPrice = overallMaxPrice,
            AllSizes = sizeNames,
            CommonWords = commonWords
        };
    }
}