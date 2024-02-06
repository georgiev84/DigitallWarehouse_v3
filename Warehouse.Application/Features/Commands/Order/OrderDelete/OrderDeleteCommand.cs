using MediatR;

namespace Warehouse.Application.Features.Commands.Order.OrderDelete;
public record OrderDeleteCommand(Guid orderId) : IRequest;

