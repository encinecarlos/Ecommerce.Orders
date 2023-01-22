namespace Order.Email.Api.Domain.Entities;

public class Email : BaseEntity<string>
{
    public string? From { get; private set; }
    public string? To { get; private set; }
    public string? Body { get; private set; }

    public void SetFrom(string origin) => this.From = origin;
    
    public void SetTo(string destination) => this.To = destination;
    
    public void SetBody(string message) => this.Body = message;
}