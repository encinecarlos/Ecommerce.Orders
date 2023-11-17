using System.Reflection;
using MediatR;
using Order.Email.Api.Application.Query;
using Order.Email.Api.Domain.Interfaces;
using Order.Email.Api.Domain.Services.EventHandler;
using Order.Email.Api.Domain.Services.Messagehandler;
using Order.Email.Api.Repositories;
using Order.Email.Api.Settings;
using SendGrid.Extensions.DependencyInjection;

namespace Order.Email.Api.Extensions;

public static class IoCExtension
{
    public static void AddIoC(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddMediatR(opt =>
        {
            opt.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
        });
        services.AddSendGrid(options =>
        {
            options.ApiKey = configuration["EmailSettings:AccessKey"];
        });
        
        services.AddSingleton<IEventHandlerService, EventHandlerService>();

        services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        services.AddSingleton<IMongoDbClientService<Domain.Entities.Email, string>, MongoDbClientService<Domain.Entities.Email, string>>();
        services.AddSingleton<IMongoDbClientService<Domain.Entities.Order, string>, MongoDbClientService<Domain.Entities.Order, string>>();
        services.AddSingleton<IOrderRepository, OrderRepository>();
        services.AddSingleton<Domain.Entities.Email>();
        services.AddSingleton<Domain.Entities.Order>();
        services.AddSingleton<GetOrderById>();
        services.AddSingleton<IMessageHandlerService, MessageHandlerService>();
    }
}