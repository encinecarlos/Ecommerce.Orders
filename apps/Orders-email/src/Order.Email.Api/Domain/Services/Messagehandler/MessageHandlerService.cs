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

    public async Task SendMessage(Entities.Email messageEmail)
    {
        Logger.LogInformation("Send email to customer");
        var plainMessage = $"your product {messageEmail?.Content?.ProductName} was purchased successfully.";
        var htmlMessage = $"your product <b>{messageEmail?.Content?.ProductName}</b> was purchased successfully.";
        
        var message = new SendGridMessage
        {
            From = new EmailAddress(Configuration["EmailSettings:Origin"], "Ecommerce Store"),
            Subject = $"ecommerce.store | purchase nº {messageEmail?.Content?.OrderId}"
        };

        message.AddContent(MimeType.Html, htmlMessage);
        message.AddContent(MimeType.Text, plainMessage);
        message.AddTo(new EmailAddress(messageEmail?.Content?.CustomerEmail, "Carlos Encine"));

        await MailClient.SendEmailAsync(message);
    }
}