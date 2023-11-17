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
            ProductName: order.Products[0].ProductName,
            ProductPrice: order.Products[0].Price);
        
        var email = new Domain.Entities.Email();
        email.SetOrigin("carlos.encine@outlook.com");
        email.SetDestination(order.Customer.Email);
        email.SetContent(content);

        return email;
    }
}