using FluentValidation;
using Warehouse.Application.Models.Constants;

namespace Warehouse.Application.Features.Commands.Product.ProductCreate;
public class ProductCreateValidator : AbstractValidator<ProductCreateCommand>
{
    public ProductCreateValidator()
    {
        RuleFor(command => command.BrandId)
             .NotEmpty().WithMessage("BrandId is required.");

        RuleFor(command => command.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");

        RuleFor(command => command.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

        RuleFor(command => command.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0.");

        RuleFor(command => command.GroupIds)
            .NotEmpty().WithMessage("At least one GroupId must be provided.");

        RuleFor(command => command.SizeInformation)
            .NotEmpty().WithMessage("At least one Size must be provided.");

    }
}
