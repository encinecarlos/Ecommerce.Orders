namespace ECommerce.Orders.Api.Domain.Entities;

public class Customer : BaseEntity<string>
{
    public Customer(
        string name,
        string address,
        string email, 
        string phone)
    {
        Name = name;
        Address = address;
        Email = email;
        Phone = phone;
    }

    public string Name { get; init; }
    public string Address { get; init; }
    public string Email { get; init; }
    public string Phone { get; init; }
}