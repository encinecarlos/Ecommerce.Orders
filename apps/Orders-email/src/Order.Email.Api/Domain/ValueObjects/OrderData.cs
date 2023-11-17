namespace Order.Email.Api.Domain.ValueObjects;

public record OrderData(
    string OrderId,
    string CustomerName,
    string CustomerEmail,
    string ProductName,
    decimal ProductPrice);