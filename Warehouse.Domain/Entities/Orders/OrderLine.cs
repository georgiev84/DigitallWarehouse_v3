﻿using Warehouse.Domain.Entities.Products;

namespace Warehouse.Domain.Entities.Orders;

public class OrderLine
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public Guid SizeId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public Order? Order { get; set; }
    public Product? Product { get; set; }
    public Size? Size { get; set; }
}