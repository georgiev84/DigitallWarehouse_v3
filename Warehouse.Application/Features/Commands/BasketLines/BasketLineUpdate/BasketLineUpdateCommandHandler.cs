using AutoMapper;
using MediatR;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Application.Models.Dto.BasketDtos;
using Warehouse.Domain.Exceptions.BasketExceptions;

namespace Warehouse.Application.Features.Commands.BasketLines.BasketLineUpdate;

public class BasketLineUpdateCommandHandler(IMapper _mapper, IUnitOfWork _unitOfWork) : IRequestHandler<BasketLineUpdateCommand, BasketLineUpdateDto>
{
    public async Task<BasketLineUpdateDto> Handle(BasketLineUpdateCommand command, CancellationToken cancellationToken)
    {
        var existingBasketLine = await _unitOfWork.BasketLines.GetById(command.BasketLineId);

        if (existingBasketLine is null)
        {
            throw new BasketLineNotFoundException($"Basket line with ID {command.BasketLineId} not found.");
        }

        _mapper.Map(command, existingBasketLine);

        await _unitOfWork.SaveAsync();

        var updatedDto = _mapper.Map<BasketLineUpdateDto>(existingBasketLine);

        return updatedDto;
    }
}