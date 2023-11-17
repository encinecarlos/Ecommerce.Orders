using Order.Email.Api.Domain.Entities;

namespace Order.Email.Api.Domain.ValueObjects;

public record OrderData(
    string OrderId,
    string CustomerName,
    string CustomerEmail,
    List<Product> Products);