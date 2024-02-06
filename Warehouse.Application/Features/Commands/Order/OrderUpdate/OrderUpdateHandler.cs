using AutoMapper;
using MediatR;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Application.Models.Dto;
using Warehouse.Domain.Exceptions;

namespace Warehouse.Application.Features.Commands.Order.OrderUpdate;
public class OrderUpdateHandler : IRequestHandler<OrderUpdateCommand, OrderUpdateDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public OrderUpdateHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<OrderUpdateDto> Handle(OrderUpdateCommand command, CancellationToken cancellationToken)
    {
        var existingOrder = await _unitOfWork.Orders.GetSingleOrderAsync(command.Id);
        if (existingOrder == null)
        {
            throw new ProductNotFoundException($"Order with ID {command.Id} not found.");
        }

        existingOrder.StatusId = command.StatusId;
        existingOrder.PaymentId = command.PaymentId;
        existingOrder.OrderDate = command.OrderDate;
        existingOrder.UserId = command.UserId;
        existingOrder.TotalAmount = command.TotalAmount;

       // Update order details
        foreach (var updatedOrderDetail in command.OrderLines)
        {
            var existingOrderDetail = existingOrder.OrderDetails.FirstOrDefault(od => od.ProductId == updatedOrderDetail.ProductId);
            if (existingOrderDetail != null)
            {
                existingOrderDetail.ProductId = updatedOrderDetail.ProductId;
                existingOrderDetail.SizeId = updatedOrderDetail.SizeId;
                existingOrderDetail.Quantity = updatedOrderDetail.Quantity;
                existingOrderDetail.Price = updatedOrderDetail.Price;
            }
        }

        _unitOfWork.Orders.Update(existingOrder);
        _unitOfWork.Save();

        var updatedOrderDto = _mapper.Map<OrderUpdateDto>(existingOrder);
        return updatedOrderDto;
    }
}
