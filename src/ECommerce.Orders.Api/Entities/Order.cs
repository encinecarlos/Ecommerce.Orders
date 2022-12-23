using ECommerce.Orders.Api.Enums;

namespace ECommerce.Orders.Api.Entities;

public class Order
{
    public Order()
    {
        OrderId = Guid.NewGuid().GetHashCode();
        OrderDate = DateTime.UtcNow;
        OrderStatus = OrderStatus.Pending;
    }
    
    public int OrderId { get; set; }
    public Customer? Customer { get; set; }
    public IList<Product>? Products { get; set; }
    public PaymentType PaymentType { get; set; }
    public decimal OrderTotal { get; set; }
    public ShippingType? ShippingType { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public DateTime? OrderDate { get; set; }
    public DateTime? ShippingDate { get; set; }
}