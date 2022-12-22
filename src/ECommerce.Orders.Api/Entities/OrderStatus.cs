namespace ECommerce.Orders.Api.Entities;

public enum OrderStatus
{
    Pending,
    InProgress,
    Shipped,
    Delivered,
    Cancelled,
    Refunded,
    Returned
}