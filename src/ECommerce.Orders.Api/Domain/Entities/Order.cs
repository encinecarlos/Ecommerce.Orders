using ECommerce.Orders.Api.Domain.ValueObjects;

namespace ECommerce.Orders.Api.Domain.Entities;

public class Order : BaseEntity<string>
{
    public Order()
    {
        Id = Guid.NewGuid().ToString();
        OrderId = DateTime.UtcNow.Ticks.ToString();
        OrderDate = DateTime.UtcNow;
        OrderStatus = OrderStatus.Pending;
    }

    public string? OrderId { get; set; }
    public Customer? Customer { get; set; }
    public IList<Product>? Products { get; set; }
    public PaymentType PaymentType { get; set; }
    public decimal OrderTotal { get; set; }
    public ShippingType? ShippingType { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public DateTime? OrderDate { get; set; }
    public DateTime? ShippingDate { get; set; }
}