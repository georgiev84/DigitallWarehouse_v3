using AutoMapper;
using Warehouse.Application.Queries.Warehouse;
using Warehouse.Application.Models.Dto;
using Warehouse.Api.Models.Responses;
using Warehouse.Api.Models.Requests;

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
        CreateMap<ProductDto, ProductResponse>();
    }
}
