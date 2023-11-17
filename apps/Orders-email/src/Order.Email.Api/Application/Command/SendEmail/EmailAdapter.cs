using Order.Email.Api.Domain.ValueObjects;

namespace Order.Email.Api.Application.Command.SendEmail;

public static class EmailAdapter
{
    public static Domain.Entities.Email Adapt(Domain.Entities.Order order)
    {
        var content = new OrderData(
            CustomerEmail: order.Customer.Email, 
            OrderId: order.OrderId,
            CustomerName: order.Customer.Name, 
            Products: order.Products);
        
        var email = new Domain.Entities.Email();
        email.SetOrigin(content.CustomerEmail);
        email.SetDestination(order.Customer.Email);
        email.SetContent(content);

        return email;
    }
}