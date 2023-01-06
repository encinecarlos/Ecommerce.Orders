namespace ECommerce.Orders.Api.Domain.Entities;

public class Product
{
    public Product(
        string productName,
        decimal price,
        int quantity)
    {
        ProductName = productName;
        Price = price;
        Quantity = quantity;
    }

    public string ProductName { get; init; }
    public decimal Price { get; init; }
    public int Quantity { get; init; }
}