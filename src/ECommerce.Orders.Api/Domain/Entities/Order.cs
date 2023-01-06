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
        Products = new List<Product>();
    }

    public string? OrderId { get; init; }
    public Customer? Customer { get; private set; }
    public List<Product>? Products { get; private set; }
    public PaymentType PaymentType { get; private set; }
    public decimal OrderTotal { get; private set; }
    public ShippingType? ShippingType { get; private set; }
    public OrderStatus OrderStatus { get; init; }
    public DateTime? OrderDate { get; init; }
    public DateTime? ShippingDate { get; private set; }

    public void SetShippingDate(DateTime shippingDate) =>
        this.ShippingDate = shippingDate;

    public void SetShippingType(ShippingType shippingType) =>
        this.ShippingType = shippingType;

    public void SetOrderTotal(decimal value) =>
        this.OrderTotal = value;

    public void SetPaymentType(PaymentType paymentType) =>
        this.PaymentType = paymentType;

    public void SetProducts(Product product)
    {
        Products.Add(product);
    }

    public void SetCustomer(Customer customer) =>
     this.Customer = customer;
}