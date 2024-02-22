using MediatR;

namespace Warehouse.Application.Features.Commands.Products.Delete;
public record ProductDeleteCommand(Guid productId) : IRequest;