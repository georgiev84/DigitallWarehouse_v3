using FluentValidation;
using Warehouse.Application.Features.Commands.Product.ProductCreate;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Features.Commands.Order.OrderCreate;
public class OrderDetailsValidator : AbstractValidator<OrderDetails>
{
    public OrderDetailsValidator()
    {
        RuleFor(x => x.OrderId).NotEmpty().WithMessage("OrderId is required.");
        RuleFor(x => x.ProductId).NotEmpty().WithMessage("ProductId is required.");
        RuleFor(x => x.SizeId).NotEmpty().WithMessage("SizeId is required.");
        RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than 0.");
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0).WithMessage("Price must be greater than or equal to 0.");
    }
}
