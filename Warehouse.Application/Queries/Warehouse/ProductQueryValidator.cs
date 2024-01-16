using FluentValidation;

namespace Warehouse.Application.Queries.Warehouse;

public sealed class ProductQueryValidator : AbstractValidator<ProductQuery>
{
    public ProductQueryValidator()
    {
        RuleFor(p => p.MinPrice)
            .Empty()
                .When(p => p.MinPrice.HasValue && p.MinPrice <= 0)
                .WithMessage("MinPrice must be greater than zero when provided")
            .Must(BeValidDecimal)
                .When(p => p.MinPrice.HasValue)
                .WithMessage("MinPrice must be a valid decimal number when provided");

        RuleFor(p => p.MaxPrice)
            .Empty()
                .When(p => p.MaxPrice.HasValue && p.MaxPrice <= 0)
                .WithMessage("MaxPrice must be greater than zero when provided")
            .Must(BeValidDecimal)
                .When(p => p.MaxPrice.HasValue)
                .WithMessage("MaxPrice must be a valid decimal number when provided");

        RuleFor(p => p.Size)
            .MaximumLength(20)
                .When(p => !string.IsNullOrEmpty(p.Size))
                .WithMessage("Size length must not exceed 50 characters");
    }

    private bool BeValidDecimal(decimal? value)
    {
        return value == null || decimal.TryParse(value.ToString(), out _);
    }
}
