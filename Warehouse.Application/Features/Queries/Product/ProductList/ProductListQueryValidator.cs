using FluentValidation;
using Warehouse.Application.Models.Constants;

namespace Warehouse.Application.Features.Queries.Product.ProductList;

public sealed class ProductListQueryValidator : AbstractValidator<ProductListQuery>
{
    public ProductListQueryValidator()
    {
        RuleFor(p => p.MinPrice)
            .Empty()
                .When(p => p.MinPrice.HasValue && p.MinPrice <= 0)
                .WithMessage(ValidationMessages.MinPriceGreaterThanZero)
            .Must(BeValidDecimal)
                 .When(p => p.MinPrice.HasValue)
                 .WithMessage(ValidationMessages.MinPriceValidDecimal);

        RuleFor(p => p.MaxPrice)
            .Empty()
                .When(p => p.MaxPrice.HasValue && p.MaxPrice <= 0)
                .WithMessage(ValidationMessages.MaxPriceGreaterThanZero)
            .Must(BeValidDecimal)
                .When(p => p.MaxPrice.HasValue)
                .WithMessage(ValidationMessages.MaxPriceValidDecimal);

        RuleFor(p => p.Size)
            .MaximumLength(20)
                .When(p => !string.IsNullOrEmpty(p.Size))
                .WithMessage(ValidationMessages.SizeMaxLength);
    }

    private bool BeValidDecimal(decimal? value)
    {
        return value == null || decimal.TryParse(value.ToString(), out _);
    }
}