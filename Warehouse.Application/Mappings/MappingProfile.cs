using AutoMapper;
using Warehouse.Application.Features.Queries.Product;
using Warehouse.Application.Models.Dto;

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
