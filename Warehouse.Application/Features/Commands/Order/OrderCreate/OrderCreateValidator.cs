using FluentValidation;
using Warehouse.Application.Models.Dto;

namespace Warehouse.Application.Features.Commands.Order.OrderCreate;
public class OrderCreateValidator : AbstractValidator<OrderCreateCommand>
{
    public OrderCreateValidator()
    {
        RuleFor(x => x.StatusId).NotEmpty().WithMessage("StatusId is required.");
        RuleFor(x => x.PaymentId).NotEmpty().WithMessage("PaymentId is required.");
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.");
        RuleFor(x => x.OrderDate).NotEmpty().WithMessage("OrderDate is required.")
                                   .Must(date => date <= DateTime.UtcNow).WithMessage("OrderDate must be in the past.");

        RuleFor(x => x.TotalAmount).GreaterThan(0).WithMessage("TotalAmount must be greater than 0.");

        RuleForEach(x => x.OrderDetails).SetValidator(new OrderDetailsValidator());
    }
}
