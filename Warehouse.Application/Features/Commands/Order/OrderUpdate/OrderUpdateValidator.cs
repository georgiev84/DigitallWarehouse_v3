using FluentValidation;

namespace Warehouse.Application.Features.Commands.Order.OrderUpdate;
public class OrderUpdateValidator : AbstractValidator<OrderUpdateCommand>
{
    public OrderUpdateValidator()
    {
        RuleFor(command => command.Id).NotEmpty().WithMessage("Order Id is required.");
    }
}
