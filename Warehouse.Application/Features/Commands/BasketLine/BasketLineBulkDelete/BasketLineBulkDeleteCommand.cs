using MediatR;

namespace Warehouse.Application.Features.Commands.BasketLine.BasketLineBulkDelete;
public record BasketLineBulkDeleteCommand(Guid UserId) : IRequest;