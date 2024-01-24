using Microsoft.Extensions.Logging;
using Warehouse.Application.Common.Interfaces;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Application.Extensions;
using Warehouse.Application.Models.Dto;
using Warehouse.Domain.Entities;
using Warehouse.Domain.Exceptions;
using Warehouse.Infrastructure.Extensions;


namespace Warehouse.Infrastructure.Services;

public class ProductService : IProductService
{
    private readonly ILogger<ProductService> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(ILogger<ProductService> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<ProductDto> GetFilteredProductsAsync(ItemsDto items)
    {
        try
        {
            LoggingExtensions.LogGettingProducts(_logger);

            // Fetch all products from DB

            var allProducts = await _unitOfWork.Products.GetAll();
            var allProducts1 = await _unitOfWork.Products.GetProductsAsync();

            //var allProducts = _productRepository
            //    .GetQueryable()
            //    .Include(p => p.ProductSizes)
            //        .ThenInclude(ps => ps.Size)
            //    .Include(p => p.Brand)
            //    .Include(p => p.ProductGroups)
            //        .ThenInclude(pg => pg.Group).AsEnumerable();



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
}
