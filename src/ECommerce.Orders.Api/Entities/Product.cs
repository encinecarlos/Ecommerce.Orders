namespace ECommerce.Orders.Api.Entities;

public class Product
{
    public string? ProductName { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}