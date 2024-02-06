using MediatR;
using Warehouse.Application.Models.Dto;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Features.Commands.Order.OrderCreate;
public record OrderCreateCommand(Guid StatusId, Guid PaymentId, DateTime OrderDate, Guid UserId, decimal TotalAmount, List<OrderLine> OrderLines) : IRequest<OrderCreateDto>;
