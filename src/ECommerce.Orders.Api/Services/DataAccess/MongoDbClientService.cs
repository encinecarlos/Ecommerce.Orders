using System.Linq.Expressions;
using ECommerce.Orders.Api.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace ECommerce.Orders.Api.Services.DataAccess;

public class MongoDbClientService<T, TId> : IMongoDbClientService<T, TId>
{
    private MongoClient Client { get; }
    private IMongoCollection<T> Collection { get; }
    private ILogger<MongoDbClientService<T, TId>> Logger { get; }

    public MongoDbClientService(IOptions<MongoDbSettings> config, ILogger<MongoDbClientService<T, TId>> logger)
    {
        Logger = logger;
        ArgumentNullException.ThrowIfNull(config.Value);

        try
        {
            var collectionName = typeof(T).Name;

            BsonClassMap.RegisterClassMap<T>(cm => cm.AutoMap());
            MongoUrl mongoUrl = new MongoUrl(config.Value.ConnectionString);
            var mongoClientSettings = MongoClientSettings.FromUrl(mongoUrl);

            Client = new MongoClient(mongoClientSettings);

            Collection = Client.GetDatabase(config.Value.Database).GetCollection<T>(collectionName);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message, Client);
            throw;
        }
    }
    
    public async Task InsertAsync(T entity)
    {
        Logger.LogInformation("send data to collection");
        await Collection.InsertOneAsync(entity);
    }

    public async Task<ReplaceOneResult> UpdateAsync(TId id, T entity)
    {
        Logger.LogInformation($"Update record on database with id: {id}");
        var filter = Builders<T>.Filter.Eq("_id", id);
        return await Collection.ReplaceOneAsync(filter, entity);
    }

    public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate)
    {
        Logger.LogInformation("Get all records of collection");
        return await ((await Collection.FindAsync(predicate)).ToListAsync());
    }

    public async Task<T> FindSingleAsync(Expression<Func<T, bool>> predicate)
    {
       Logger.LogInformation("Get single record from collection");
       return await ((await Collection.FindAsync(predicate)).FirstOrDefaultAsync());
    }

    public async Task RemoveAsync(TId id)
    {
        Logger.LogInformation($"Remove resource by Id: {id}");
        await Collection.DeleteOneAsync(Builders<T>.Filter.Eq("_id", id));
    }
}