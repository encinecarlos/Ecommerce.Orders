namespace Order.Email.Api.Domain.ValueObjects;

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