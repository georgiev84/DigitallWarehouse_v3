using AutoMapper;
using Microsoft.Extensions.Logging;
using Warehouse.Application.Common.Interfaces;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Application.Extensions;
using Warehouse.Application.Features.Commands.Product;
using Warehouse.Application.Models.Dto;
using Warehouse.Domain.Entities;
using Warehouse.Domain.Exceptions;
using Warehouse.Infrastructure.Extensions;


namespace Warehouse.Infrastructure.Services;

public class ProductService : IProductService
{
    private readonly ILogger<ProductService> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductService(ILogger<ProductService> logger, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ProductDto> GetFilteredProductsAsync(ItemsDto items)
    {
        try
        {
            LoggingExtensions.LogGettingProducts(_logger);

            // Fetch all products from DB
            var rawProducts = await _unitOfWork.Products.GetProductsDetailsAsync();

            var allProducts = _mapper.Map<IEnumerable<ProductDetailsDto>>(rawProducts);

            if (allProducts == null)
            {
                LoggingExtensions.LogErrorFetchingProducts(_logger);
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
            LoggingExtensions.LogFilteringProducts(_logger);
            var filteredProducts = allProducts;

            filteredProducts = filteredProducts
                .FilterByMinPrice(items.MinPrice)
                .FilterByMaxPrice(items.MaxPrice)
                .FilterBySize(items.Size)
                .HighlightWords(items.Highlight);



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
        catch (Exception ex)
        {
            LoggingExtensions.LogErrorFetchingProducts(_logger, ex.Message);
            throw;
        }
    }

    public async Task<CreateProductDetailsDto> CreateProductAsync(CreateProductCommand command)
    {

        var product = new Product
        {
            Title = command.Title,
            Description = command.Description,
            Price = command.Price,
            BrandId = command.BrandId,
            ProductSizes = command.SizeInformation.Select(size => new ProductSize
            {
                SizeId = size.SizeId,
                QuantityInStock = size.Quantity
            }).ToList(),
            ProductGroups = command.GroupIds.Select(groupId => new ProductGroup
            {
                GroupId = groupId
            }).ToList()
        };

        await _unitOfWork.Products.Add(product);
        _unitOfWork.Save();

        var checkedProduct = await _unitOfWork.Products.GetProductDetailsByIdAsync(product.Id);
        if (checkedProduct == null)
        {
            throw new ProductCreationException("Failed to create product.");
        }
        var productDto = _mapper.Map<CreateProductDetailsDto>(checkedProduct);

        return productDto;
    }
}
