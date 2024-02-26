using MediatR;

namespace Warehouse.Application.Features.Commands.Orders.OrderDelete;
public record OrderDeleteCommand(Guid orderId) : IRequest;