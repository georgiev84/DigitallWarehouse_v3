using AutoMapper;
using Warehouse.Application.Queries.Warehouse;
using Warehouse.Api.Models;
using Warehouse.Application.Models.Dto;
using Warehouse.Api.Dto;

namespace Warehouse.Api.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        MapFromRequestToQueriesOrCommands();
        MapFromDtoToResponse();
    }

    private void MapFromRequestToQueriesOrCommands()
    {
        CreateMap<ProductFilterRequest, ProductQuery>();
    }

    private void MapFromDtoToResponse()
    {
        CreateMap<ProductResponseDto, ProductDto>();
    }
}
