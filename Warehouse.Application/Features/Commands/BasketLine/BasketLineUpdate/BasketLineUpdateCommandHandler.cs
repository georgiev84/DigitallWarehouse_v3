using AutoMapper;
using MediatR;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Application.Models.Dto.BasketDtos;
using Warehouse.Domain.Exceptions;

namespace Warehouse.Application.Features.Commands.BasketLine.BasketLineUpdate;

public class BasketLineUpdateCommandHandler : IRequestHandler<BasketLineUpdateCommand, BasketLineUpdateDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public BasketLineUpdateCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<BasketLineUpdateDto> Handle(BasketLineUpdateCommand command, CancellationToken cancellationToken)
    {
        var existingBasketLine = await _unitOfWork.BasketLines.GetById(command.BasketLineId);

        if (existingBasketLine == null)
        {
            throw new BasketLineNotFoundException($"Basket line with ID {command.BasketLineId} not found.");
        }

        _mapper.Map(command, existingBasketLine);

        await _unitOfWork.SaveAsync();

        var updatedDto = _mapper.Map<BasketLineUpdateDto>(existingBasketLine);

        return updatedDto;
    }
}