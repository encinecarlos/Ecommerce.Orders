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
    
    public async Task InsertAsync(T entity)
    {
        _logger.LogInformation("Save data on database.");
        await _collection.InsertOneAsync(entity);
    }

    public async Task<ReplaceOneResult> UpdateAsync(TId id, T entity)
    {
        _logger.LogInformation($"update document wuth id: {id}");
        var filter = Builders<T>.Filter.Eq("_id", id);
        return await _collection.ReplaceOneAsync(filter, entity);
    }

    public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate)
    {
        _logger.LogInformation("Get all documents from database");
        return await (await _collection.FindAsync(predicate)).ToListAsync();
    }

    public async Task<T> FindSingleAsync(Expression<Func<T, bool>> predicate)
    {
        _logger.LogInformation("find single record.");
        return await (await _collection.FindAsync(predicate)).FirstOrDefaultAsync();
    }

    public async Task Removeasync(TId id)
    {
        _logger.LogInformation($"Remove document with id: {id}");
        await _collection.DeleteOneAsync(Builders<T>.Filter.Eq("_id", id));
    }
}