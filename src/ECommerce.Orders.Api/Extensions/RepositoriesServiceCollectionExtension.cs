using ECommerce.Orders.Api.Entities;
using ECommerce.Orders.Api.Repositories;
using ECommerce.Orders.Api.Services;
using ECommerce.Orders.Api.Settings;
using Microsoft.Extensions.Options;

namespace ECommerce.Orders.Api.Extensions;

public static class RepositoriesServiceCollectionExtension
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        // var dbSettings = new MongoDbSettings()
        // {
        //     ConnectionString = configuration["Mongodb:ConnectionString"],
        //     Database = configuration["MongoDb:Database"]
        // };

        services.Configure<MongoDbSettings>(configuration.GetSection("MongoDb"));
        services.AddScoped<IMongoDbClientService<Order, string>, MongoDbClientService<Order, string>>();
        services.AddScoped<IOrdersRepository, OrdersRepository>();
        
        return services;
    }
}