namespace Order.Email.Api.Domain.Interfaces;

public interface IOrderRepository
{
    Task<Entities.Order> GetOrderByIdAsync(string id);
    Task<bool> UpdateOrderAsync(string orderId, Entities.Order order);
}