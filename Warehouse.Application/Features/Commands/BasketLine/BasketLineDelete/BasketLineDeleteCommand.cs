using MediatR;

namespace Warehouse.Application.Features.Commands.BasketLine.BasketLineDelete;
public record BasketLineDeleteCommand(Guid BasketLineId) : IRequest;