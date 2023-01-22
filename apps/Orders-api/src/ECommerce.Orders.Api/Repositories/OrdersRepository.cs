using ECommerce.Orders.Api.Domain.Entities;
using ECommerce.Orders.Api.Domain.Interfaces;

namespace ECommerce.Orders.Api.Repositories;

public class OrdersRepository : IOrdersRepository
{
    public IMongoDbClientService<Order, string> Client { get; set; }

    public OrdersRepository(IMongoDbClientService<Order, string> client)
    {
        Client = client;
    }

    public async Task AddOrderAsync(Order order)
    {
        await Client.InsertAsync(order);
    }

    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        return await Client.FindAllAsync(_ => true);
    }

    public async Task<Order> GetOrderByIdAsync(string id)
    {
        return await Client.FindSingleAsync(x => x.Id == id);
    }

    public async Task<bool> UpdateOrderAsync(Order order)
    {
        var update = await Client.UpdateAsync(order.Id, order);
        return update.IsAcknowledged;
    }

    public async Task RemoveOrderAsync(string id)
    {
        await Client.RemoveAsync(id);
    }
}