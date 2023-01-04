using ECommerce.Orders.Api.Domain.Interfaces;
using ECommerce.Orders.Api.Repositories;
using ECommerce.Orders.Api.Settings;

namespace ECommerce.Orders.Api.Extensions;

public static class RepositoriesServiceCollectionExtension
{
    public static IServiceCollection AddRepositories(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<MongoDbSettings>(configuration.GetSection("MongoDb"));
        services.AddSingleton<IMongoDbClientService<Domain.Entities.Order, string>, MongoDbClientService<Domain.Entities.Order, string>>();
        services.AddScoped<IOrdersRepository, OrdersRepository>();

        return services;
    }
}