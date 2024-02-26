using MediatR;

namespace Warehouse.Application.Features.Commands.BasketLines.BasketLineBulkDelete;
public record BasketLineBulkDeleteCommand(Guid UserId) : IRequest;