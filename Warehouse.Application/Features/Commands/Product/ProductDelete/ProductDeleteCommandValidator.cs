using FluentValidation;
using Warehouse.Application.Features.Commands.Product.Delete;

namespace Warehouse.Application.Features.Commands.Product.ProductDelete;


public class ProductDeleteCommandValidator : AbstractValidator<ProductDeleteCommand>
{
    public ProductDeleteCommandValidator()
    {
        RuleFor(command => command.productId).NotEmpty().WithMessage("Product ID is required.");
    }
}

