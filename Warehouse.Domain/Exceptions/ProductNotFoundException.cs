﻿namespace Warehouse.Domain.Exceptions;

public class ProductNotFoundException : Exception
{
    public ProductNotFoundException(string message)
     : base(message)
    {
    }
}