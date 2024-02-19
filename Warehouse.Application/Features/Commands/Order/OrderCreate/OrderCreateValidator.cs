using FluentValidation;
using Warehouse.Application.Common.Validation;

namespace Warehouse.Application.Features.Commands.Order.OrderCreate;

public class OrderCreateValidator : AbstractValidator<OrderCreateCommand>
{
    public OrderCreateValidator()
    {
        RuleFor(x => x.StatusId).NotEmpty().WithMessage(ValidationMessages.RequiredId(nameof(OrderCreateCommand.StatusId)));

        RuleFor(x => x.PaymentId).NotEmpty().WithMessage(ValidationMessages.RequiredId(nameof(OrderCreateCommand.PaymentId)));

        RuleFor(x => x.UserId).NotEmpty().WithMessage(ValidationMessages.RequiredId(nameof(OrderCreateCommand.UserId)));

        RuleFor(x => x.OrderDate).NotEmpty().WithMessage(ValidationMessages.RequiredId(nameof(OrderCreateCommand.OrderDate)))
                                   .Must(date => date <= DateTime.UtcNow).WithMessage(ValidationMessages.RequiredOrderDateInPast);

        RuleFor(x => x.TotalAmount).GreaterThan(0).WithMessage(ValidationMessages.ItemBiggerThanZero(nameof(OrderCreateCommand.TotalAmount)));

        RuleForEach(x => x.OrderLines).SetValidator(new OrderLineValidator());
    }
}