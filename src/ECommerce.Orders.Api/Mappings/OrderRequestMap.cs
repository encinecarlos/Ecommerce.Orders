﻿using ECommerce.Orders.Api.Entities;
using ECommerce.Orders.Api.Enums;

namespace ECommerce.Orders.Api.Mappings;

public class OrderRequestMap
{
    public Customer? Customer { get; set; }
    public IList<Product>? Products { get; set; }
    public PaymentType PaymentType { get; set; }
    public decimal OrderTotal { get; set; }
    public ShippingType? ShippingType { get; set; }
}