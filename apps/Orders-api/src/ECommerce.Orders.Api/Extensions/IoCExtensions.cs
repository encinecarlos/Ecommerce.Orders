using ECommerce.Orders.Api.Domain.Interfaces;
using ECommerce.Orders.Api.Domain.Services.EventHandler;
using ECommerce.Orders.Api.Repositories;
using ECommerce.Orders.Api.Settings;
using MediatR;
using System.Reflection;

namespace ECommerce.Orders.Api.Extensions;

public static class IoCExtensions
{
    public static void AddIoC(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddMediatR(opt =>
        {
            opt.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
        });

        services.AddScoped<IEventHandlerService, EventHandlerService>();

        services.Configure<MongoDbSettings>(configuration.GetSection("MongoDb"));
        services.AddSingleton<IMongoDbClientService<Domain.Entities.Order, string>, MongoDbClientService<Domain.Entities.Order, string>>();
        services.AddScoped<IOrdersRepository, OrdersRepository>();
    }
}