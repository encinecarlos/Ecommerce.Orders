using ECommerce.Orders.Api.Domain.Entities;
using ECommerce.Orders.Api.Domain.ValueObjects;

namespace ECommerce.Orders.Api.Application.Mappings;

public struct OrderRequestMap
{
    public Customer? Customer { get; set; }
    public IList<Product>? Products { get; set; }
    public PaymentType PaymentType { get; set; }
    public decimal OrderTotal { get; set; }
    public ShippingType? ShippingType { get; set; }
}