using MediatR;
using Warehouse.Application.Models.Dto;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Features.Commands.Order.OrderUpdate;
public record OrderUpdateCommand(
    Guid Id, 
    Guid StatusId, 
    Guid PaymentId, 
    DateTime OrderDate, 
    Guid UserId, 
    decimal TotalAmount, 
    List<OrderDetails> OrderLines) : IRequest<OrderUpdateDto>;

