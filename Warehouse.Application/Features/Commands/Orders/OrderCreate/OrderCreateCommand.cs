using MediatR;
using Warehouse.Application.Models.Dto.OrderDtos;

namespace Warehouse.Application.Features.Commands.Orders.OrderCreate;
public record OrderCreateCommand(
    Guid StatusId,
    Guid PaymentId,
    DateTime OrderDate,
    Guid UserId,
    decimal TotalAmount,
    List<OrderLineDto> OrderLines) : IRequest<OrderCreateDto>;