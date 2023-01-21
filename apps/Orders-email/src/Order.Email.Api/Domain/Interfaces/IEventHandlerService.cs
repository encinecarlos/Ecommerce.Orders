namespace Order.Email.Api.Domain.Interfaces;

public interface IEventHandlerService
{
    Task<string> ConsumeMessage(CancellationToken cancellationToken);
}