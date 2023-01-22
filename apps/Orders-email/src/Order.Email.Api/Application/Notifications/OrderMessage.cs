namespace Order.Email.Api.Application.Notifications;

public class OrderMessage
{
    public string? OrderId { get; init; }
    public DateTime OrderDate { get; init; }
}