using MediatR;

namespace Warehouse.Application.Features.Commands.BasketLines.BasketLineDelete;
public record BasketLineDeleteCommand(Guid BasketLineId) : IRequest;