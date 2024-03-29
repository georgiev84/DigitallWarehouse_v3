﻿namespace Warehouse.Application.Models.Dto.OrderDtos;

public class OrderCreateDto
{
    public Guid Id { get; set; }
    public string Status { get; set; }
    public DateTime OrderDate { get; set; }
    public string FullName { get; set; }
    public decimal TotalAmount { get; set; }
}