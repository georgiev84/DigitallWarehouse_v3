using AutoMapper;
using MediatR;
using Warehouse.Application.Common.Interfaces;
using Warehouse.Application.Common.Interfaces.Factories;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Application.Models.Dto;

namespace Warehouse.Application.Features.Commands.Product.ProductCreate;
public class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommand, CreateProductDetailsDto>
{
    private readonly IProductService _productService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductFactory _productFactory;

    public ProductCreateCommandHandler(IProductService productService, IMapper mapper, IUnitOfWork unitOfWork, IProductFactory productFactory)
    {
        _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _productFactory = productFactory ?? throw new ArgumentNullException(nameof(productFactory));
    }

    public async Task<CreateProductDetailsDto> Handle(ProductCreateCommand command, CancellationToken cancellationToken)
    {
        // var result = await _productService.CreateProductAsync(command);

        var product = _productFactory.CreateProduct(command);

        await _unitOfWork.Products.Add(product);
        _unitOfWork.Save();

        var checkedProduct = await _unitOfWork.Products.GetProductDetailsByIdAsync(product.Id);

        var productDto = _mapper.Map<CreateProductDetailsDto>(checkedProduct);

        return productDto;
    }
}
