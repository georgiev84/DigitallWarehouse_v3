﻿using System.ComponentModel.DataAnnotations;

namespace Warehouse.Domain.Entities;
public class Payment
{
    public Guid PaymentId { get; set; }

    public Guid OrderId { get; set; }

    public decimal Amount { get; set; }

    public string PaymentMethod { get; set; }

    public DateTime PaymentDate { get; set; }

    public Order Order { get; set; }
}