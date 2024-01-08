using MediatR;
using Warehouse.Domain.Entities;
using Warehouse.Domain.Responses;

namespace Warehouse.Application.Queries.Warehouse;

public class ProductQuery : IRequest<ProductResponse>
{
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public string Highlight { get; set; }
    public string Size { get; set; }
}
