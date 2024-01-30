﻿namespace Warehouse.Api.Models.Requests;

public class ProductUpdateRequest
{
    public Guid Id { get; set; }
    public Guid BrandId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public List<Guid> GroupIds { get; set; }
    public List<SizeInformationRequest> SizeInformation { get; set; }
}
