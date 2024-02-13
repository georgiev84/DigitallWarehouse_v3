using MediatR;

namespace Warehouse.Application.Features.Commands.Product.Delete;
public record ProductDeleteCommand(Guid productId) : IRequest;