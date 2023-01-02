namespace ECommerce.Orders.Api.Entities;

public class Customer : BaseEntity<string>
{
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
}