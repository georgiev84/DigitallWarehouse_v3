using AutoMapper;
using Warehouse.Application.Models.Dto;
using Warehouse.Application.Queries.Warehouse;

namespace Warehouse.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        MapFromQueryToDto();
    }

    private void MapFromQueryToDto()
    {
        CreateMap<ProductQuery, ItemsDto>();
    }
}
