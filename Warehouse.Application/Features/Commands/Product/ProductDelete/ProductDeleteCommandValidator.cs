using FluentValidation;
using Warehouse.Application.Common.Validation;
using Warehouse.Application.Features.Commands.Product.Delete;

namespace Warehouse.Application.Features.Commands.Product.ProductDelete;

public class ProductDeleteCommandValidator : AbstractValidator<ProductDeleteCommand>
{
    public ProductDeleteCommandValidator()
    {
        RuleFor(command => command.productId).NotEmpty().WithMessage(ValidationMessages.RequiredId(nameof(ProductDeleteCommand.productId)));
    }
}