using AutoMapper;
using Warehouse.Application.Models.Dto;
using Warehouse.Api.Models.Responses;
using Warehouse.Api.Models.Requests;
using Warehouse.Application.Features.Queries.Product;
using Warehouse.Application.Features.Commands.Product;

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
        CreateMap<ProductCreateRequest, CreateProductCommand>();
        CreateMap<ProductUpdateRequest, UpdateProductCommand>();
    }

    private void MapFromDtoToResponse()
    {
        CreateMap<ProductDto, ProductResponse>();
        CreateMap<SizeInformationRequest, SizeInformationDto>();
        CreateMap<SizeDto, SizeResponse>();
        CreateMap<CreateProductDetailsDto, CreateProductResponse>();
        CreateMap<UpdateProductDetailsDto, UpdateProductResponse>();
    }
}
