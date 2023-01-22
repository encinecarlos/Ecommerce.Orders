using System.Linq.Expressions;
using MongoDB.Driver;

namespace Order.Email.Api.Domain.Interfaces;

public interface IMongoDbClientService<T, TId>
{
    Task InsertAsync(T entity);
    Task<ReplaceOneResult> UpdateAsync(TId id, T entity);
    Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate);
    Task<T> FindSingleAsync(Expression<Func<T, bool>> predicate);
    Task Removeasync(TId id);
}