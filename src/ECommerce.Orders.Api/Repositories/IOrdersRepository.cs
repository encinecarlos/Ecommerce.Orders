using ECommerce.Orders.Api.Entities;

namespace ECommerce.Orders.Api.Repositories;

public interface IOrdersRepository
{
    public Task AddOrderAsync(Order order);
    public Task<IEnumerable<Order>> GetAllOrdersAsync();
    public Task<Order> GetOrderByIdAsync(string id);
    public Task<bool> UpdateOrderAsync(Order order);
    public Task RemoveOrderAsync(string id);
}