﻿namespace Warehouse.Application.Models.Dto;

public class SizeDto
{
    public string Name { get; set; }
    public int QuantityInStock { get; set; }
    public Guid SizeId { get; set; }
}