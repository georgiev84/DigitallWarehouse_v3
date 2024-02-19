using AutoMapper;
using MediatR;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Application.Models.Dto.OrderDtos;
using Warehouse.Domain.Exceptions;

namespace Warehouse.Application.Features.Commands.Order.OrderUpdate;

public class OrderUpdateCommandHandler : IRequestHandler<OrderUpdateCommand, OrderUpdateDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public OrderUpdateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<OrderUpdateDto> Handle(OrderUpdateCommand command, CancellationToken cancellationToken)
    {
        var existingOrder = await _unitOfWork.Orders.GetSingleOrderAsync(command.Id);
        if (existingOrder is null)
        {
            throw new ProductNotFoundException($"Order with ID {command.Id} not found.");
        }

        //existingOrder.StatusId = command.StatusId;
        //existingOrder.PaymentId = command.PaymentId;

        _mapper.Map(command, existingOrder);

        _unitOfWork.Orders.Update(existingOrder);
        await _unitOfWork.SaveAsync();

        var updatedOrderDto = _mapper.Map<OrderUpdateDto>(existingOrder);
        return updatedOrderDto;
    }
}