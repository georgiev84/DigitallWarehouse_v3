using AutoMapper;
using MediatR;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Application.Models.Dto.OrderDtos;
using Warehouse.Domain.Exceptions;

namespace Warehouse.Application.Features.Commands.Orders.OrderUpdate;

public class OrderUpdateCommandHandler(IMapper _mapper, IUnitOfWork _unitOfWork) : IRequestHandler<OrderUpdateCommand, OrderUpdateDto>
{
    public async Task<OrderUpdateDto> Handle(OrderUpdateCommand command, CancellationToken cancellationToken)
    {
        var existingOrder = await _unitOfWork.Orders.GetSingleOrderAsync(command.Id);
        if (existingOrder is null)
        {
            throw new ProductNotFoundException($"Order with ID {command.Id} not found.");
        }

        _mapper.Map(command, existingOrder);

        await _unitOfWork.Orders.Update(existingOrder);
        await _unitOfWork.SaveAsync();

        var updatedOrderDto = _mapper.Map<OrderUpdateDto>(existingOrder);
        return updatedOrderDto;
    }
}