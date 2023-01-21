using Order.Email.Api.Domain.Interfaces;

namespace Order.Email.Api.Repositories;

public class OrderRepository : IOrderRepository
{
    private IMongoDbClientService<Domain.Entities.Order, string> Client { get; }

    public OrderRepository(IMongoDbClientService<Domain.Entities.Order, string> client)
    {
        Client = client;
    }
    
    public async Task<Domain.Entities.Order> GetOrderByIdAsync(string id)
    {
        return await Client.FindSingleAsync(order => order.Id == id);
    }

    public async Task<bool> UpdateOrderAsync(string id, Domain.Entities.Order order)
    {
        var result = await Client.UpdateAsync(id, order);

        return result.IsAcknowledged;
    }
}