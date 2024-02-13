using FluentValidation;

namespace Warehouse.Application.Features.Commands.Order.OrderDelete;

public class OrderDeleteCommandValidator : AbstractValidator<OrderDeleteCommand>
{
    public OrderDeleteCommandValidator()
    {
        RuleFor(command => command.orderId).NotEmpty().WithMessage("Order ID is required.");
    }
}