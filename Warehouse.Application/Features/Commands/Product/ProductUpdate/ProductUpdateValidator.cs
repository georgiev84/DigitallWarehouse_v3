using FluentValidation;

namespace Warehouse.Application.Features.Commands.Product.Update;

public class ProductUpdateValidator : AbstractValidator<ProductUpdateCommand>
{
    public ProductUpdateValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
}