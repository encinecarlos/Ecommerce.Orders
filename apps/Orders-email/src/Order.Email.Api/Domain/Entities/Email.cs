using Order.Email.Api.Domain.ValueObjects;

namespace Order.Email.Api.Domain.Entities;

public class Email : BaseEntity<string>
{
    public string? Origin { get; private set; }
    public string? Destination { get; private set; }
    public OrderData? Content { get; private set; }

    public void SetOrigin(string origin) => this.Origin = origin;
    
    public void SetDestination(string destination) => this.Destination = destination;
    
    public void SetContent(OrderData message) => this.Content = message;
}