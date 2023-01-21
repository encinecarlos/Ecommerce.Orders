using Order.Email.Api.Domain.Interfaces;

namespace Order.Email.Api.Domain.Services.Messagehandler;

public class MessageHandlerService : IMessageHandlerService
{
    public Task SendMessage(Entities.Order order)
    {
        throw new NotImplementedException();
    }
}