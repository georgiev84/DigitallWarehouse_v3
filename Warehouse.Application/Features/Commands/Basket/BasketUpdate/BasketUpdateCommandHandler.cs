using AutoMapper;
using MediatR;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Application.Models.Dto;
using Warehouse.Domain.Exceptions;

namespace Warehouse.Application.Features.Commands.Basket.BasketUpdate;
public class BasketUpdateCommandHandler : IRequestHandler<BasketUpdateCommand, BasketUpdateDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public BasketUpdateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<BasketUpdateDto> Handle(BasketUpdateCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
