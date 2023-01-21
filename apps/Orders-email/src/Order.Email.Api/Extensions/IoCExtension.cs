using System.Reflection;
using MediatR;
using Order.Email.Api.Domain.Interfaces;
using Order.Email.Api.Domain.Services;
using Order.Email.Api.Repositories;
using Order.Email.Api.Settings;

namespace Order.Email.Api.Extensions;

public static class IoCExtension
{
    public static void AddIoC(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        
        services.AddScoped<IEventHandlerService, EventHandlerService>();

        services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        services.AddSingleton<IMongoDbClientService<Domain.Entities.Email, string>, MongoDbClientService<Domain.Entities.Email, string>>();
        services.AddSingleton<IMongoDbClientService<Domain.Entities.Order, string>, MongoDbClientService<Domain.Entities.Order, string>>();
        services.AddScoped<Domain.Entities.Email>();
        services.AddScoped<Domain.Entities.Order>();
    }
}