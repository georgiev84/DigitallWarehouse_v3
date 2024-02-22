using FluentValidation;
using Warehouse.Application.Common.Validation;

namespace Warehouse.Application.Features.Commands.Orders.OrderUpdate;

public class OrderUpdateValidator : AbstractValidator<OrderUpdateCommand>
{
    public OrderUpdateValidator()
    {
        RuleFor(command => command.Id).NotEmpty().WithMessage(ValidationMessages.RequiredId(nameof(OrderUpdateCommand.Id)));
    }
}