using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Warehouse.Application.Common.Interfaces.Factories;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Application.Extensions;
using Warehouse.Application.Models.Dto.ProductDtos;

namespace Warehouse.Application.Features.Commands.Product.ProductCreate;

public class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommand, ProductCreateDetailsDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductFactory _productFactory;
    private readonly ILogger<ProductCreateCommandHandler> _logger;

    public ProductCreateCommandHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IProductFactory productFactory,
        ILogger<ProductCreateCommandHandler> logger)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _productFactory = productFactory ?? throw new ArgumentNullException(nameof(productFactory));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<ProductCreateDetailsDto> Handle(ProductCreateCommand command, CancellationToken cancellationToken)
    {
        _logger.LogCreateMessage(command);
        var product = _productFactory.CreateProduct(command);

        await _unitOfWork.Products.Add(product);
        await _unitOfWork.SaveAsync();

        var checkedProduct = await _unitOfWork.Products.GetProductDetailsByIdAsync(product.Id);

        var productDto = _mapper.Map<ProductCreateDetailsDto>(checkedProduct);

        return productDto;
    }
}