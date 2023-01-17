using System.Linq.Expressions;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Order.Email.Api.Domain.Interfaces;
using Order.Email.Api.Settings;

namespace Order.Email.Api.Repositories;

public class MongoDbClientService<T, TId> : IMongoDbClientService<T, TId>
{
    private MongoClient Client { get; }
    private IMongoCollection<T> Collection { get; }
    private ILogger<MongoDbClientService<T, TId>> Logger { get; }

    public MongoDbClientService(
        IOptions<MongoDbSettings> config, 
        ILogger<MongoDbClientService<T, TId>> logger)
    {
        Logger = logger;
        ArgumentNullException.ThrowIfNull(config.Value);

        var collectionName = typeof(T).Name;

        BsonClassMap.RegisterClassMap<T>(cm => cm.AutoMap());
        MongoUrl url = new(config.Value.ConnectionString);
        MongoClientSettings settings = MongoClientSettings.FromUrl(url);

        Client = new(settings);
        Collection = Client.GetDatabase(config.Value.Database).GetCollection<T>(collectionName);
    }
    
    public async Task InsertAsync(T entity)
    {
        Logger.LogInformation("Save data on database.");
        await Collection.InsertOneAsync(entity);
    }

    public async Task<ReplaceOneResult> UpdateAsync(TId id, T entity)
    {
        Logger.LogInformation($"update document wuth id: {id}");
        var filter = Builders<T>.Filter.Eq("_id", id);
        return await Collection.ReplaceOneAsync(filter, entity);
    }

    public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate)
    {
        Logger.LogInformation("Get all documents from database");
        return await (await Collection.FindAsync(predicate)).ToListAsync();
    }

    public async Task<T> FindSingleAsync(Expression<Func<T, bool>> predicate)
    {
        Logger.LogInformation("find single record.");
        return await (await Collection.FindAsync(predicate)).FirstOrDefaultAsync();
    }

    public async Task Removeasync(TId id)
    {
        Logger.LogInformation($"Remove document with id: {id}");
        await Collection.DeleteOneAsync(Builders<T>.Filter.Eq("_id", id));
    }
}