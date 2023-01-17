using System.Linq.Expressions;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Order.Email.Api.Domain.Interfaces;
using Order.Email.Api.Settings;

namespace Order.Email.Api.Domain.Services;

public class MongoDbClientService<T, TId> : IMongoDbClientService<T, TId>
{
    private MongoClient _client { get; }
    private IMongoCollection<T> _collection { get; }
    private ILogger<MongoDbClientService<T, TId>> _logger { get; }

    public MongoDbClientService(
        IOptions<MongoDbSettings> config, 
        ILogger<MongoDbClientService<T, TId>> logger)
    {
        _logger = logger;
        ArgumentNullException.ThrowIfNull(config.Value);

        var collectionName = typeof(T).Name;

        BsonClassMap.RegisterClassMap<T>(cm => cm.AutoMap());
        MongoUrl url = new(config.Value.ConnectionString);
        MongoClientSettings settings = MongoClientSettings.FromUrl(url);

        _client = new(settings);
        _collection = _client.GetDatabase(config.Value.Database).GetCollection<T>(collectionName);
    }
    
    public Task InsertAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public Task<ReplaceOneResult> UpdateAsync(TId id, T entity)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<T> FindSingleAsync(Expression<Func<T>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task Removeasync(TId id)
    {
        throw new NotImplementedException();
    }
}