using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WatchIt.DTO.Query;

namespace WatchIt.WebAPI.Repositories;

public interface IRepository<T> where T : class
{
    public Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IQueryable<T>>? additionalIncludes = null);
    public Task UpdateAsync(T entity, Action<T> updateFunc);
    public Task AddAsync(T entity);
    public Task<T> UpdateOrAddAsync(T? entity, Func<T> addFunc, Action<T> updateFunc);
    public Task DeleteAsync(T entity);
    public Task DeleteAsync(Expression<Func<T, bool>> predicate);
}