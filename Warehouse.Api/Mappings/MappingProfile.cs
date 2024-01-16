using AutoMapper;
using Warehouse.Application.Queries.Warehouse;
using Warehouse.Api.Models;

namespace Warehouse.Api.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ProductFilterModelDto, ProductQuery>();
    }
}
