using Order.Email.Api.Domain.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Order.Email.Api.Domain.Services.Messagehandler;

public class MessageHandlerService : IMessageHandlerService
{
    private IConfiguration Configuration { get; }
    private ILogger<MessageHandlerService> Logger { get; }
    private ISendGridClient MailClient { get; }
    public MessageHandlerService(IConfiguration configuration, ILogger<MessageHandlerService> logger, ISendGridClient mailclient)
    {
        Configuration = configuration;
        Logger = logger;
        MailClient = mailclient;
    }

    public async Task SendMessage(Entities.Order order)
    {
        Logger.LogInformation("Send email to customer");
        var plainMessage = $"your product {order.Products[0].ProductName} was purchased successfully.";
        var htmlMessage = $"your product <b>{order.Products[0].ProductName}</b> was purchased successfully.";
        
        var message = new SendGridMessage
        {
            From = new EmailAddress(Configuration["EmailSettings:Origin"], "Ecommerce Store"),
            Subject = $"ecommerce.store | purchase nº {order.OrderId}"
        };

        message.AddContent(MimeType.Html, htmlMessage);
        message.AddContent(MimeType.Text, plainMessage);
        message.AddTo(new EmailAddress(order.Customer.Email, "Carlos Encine"));

        await MailClient.SendEmailAsync(message);
    }
}