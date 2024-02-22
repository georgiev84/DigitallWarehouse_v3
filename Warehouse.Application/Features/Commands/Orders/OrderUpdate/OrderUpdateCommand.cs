using MediatR;
using Warehouse.Application.Models.Dto.OrderDtos;

namespace Warehouse.Application.Features.Commands.Orders.OrderUpdate;
public record OrderUpdateCommand(
    Guid Id,
    Guid StatusId,
    Guid PaymentId) : IRequest<OrderUpdateDto>;