using MediatR;
using Warehouse.Application.Models.Dto;
using Warehouse.Application.Models.Dto.BasketDtos;

namespace Warehouse.Application.Features.Commands.BasketLine.BasketLineCreate;
public record BasketLineCreateCommand(
    Guid UserId,
    BasketLineDto? BasketLine) : IRequest<BasketLineCreateDto>;
