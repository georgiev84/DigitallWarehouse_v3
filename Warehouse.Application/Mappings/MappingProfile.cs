using AutoMapper;
using Warehouse.Application.Models.Dto;
using Warehouse.Domain.Responses;

namespace Warehouse.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ProductDomainModel, ProductResponseDto>();
    }
}
