using ECommerce.Orders.Api.Domain.Entities;

namespace ECommerce.Orders.Api.Domain.Interfaces;

public interface IOrdersRepository
{
    public Task AddOrderAsync(Order order);
    public Task<IEnumerable<Order>> GetAllOrdersAsync();
    public Task<Order> GetOrderByIdAsync(string id);
    public Task<bool> UpdateOrderAsync(Order order);
    public Task RemoveOrderAsync(string id);
}