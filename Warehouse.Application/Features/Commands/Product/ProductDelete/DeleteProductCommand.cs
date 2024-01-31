using MediatR;
using Warehouse.Application.Models.Dto;

namespace Warehouse.Application.Features.Commands.Product.Delete;
public record DeleteProductCommand(Guid productId) : IRequest;
