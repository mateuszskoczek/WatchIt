using System.Collections.Immutable;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WatchIt.Database;
using WatchIt.DTO.Query;

namespace WatchIt.WebAPI.Repositories;


public abstract class Repository<T> : IRepository<T> where T : class
{
    #region SERVICES

    protected readonly DatabaseContext Database;
    
    #endregion
    
    
    
    #region FIELDS
    
    protected readonly DbSet<T> DefaultSet;
    
    #endregion
    
    
    
    #region CONSTRUCTORS

    protected Repository(DatabaseContext database)
    {
        Database = database;
        DefaultSet = database.Set<T>();
    }

    #endregion
    
    
    
    #region PUBLIC METHODS
    
    public async Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IQueryable<T>>? additionalIncludes = null) => 
        await GetAllAsync<T>(additionalIncludes);

    public async Task UpdateAsync(T entity, Action<T> updateFunc) =>
        await UpdateAsync<T>(entity, updateFunc);

    public async Task AddAsync(T entity) =>
        await AddAsync<T>(entity);
    
    public async Task<T> UpdateOrAddAsync(T? entity, Func<T> addFunc, Action<T> updateFunc) =>
        await UpdateOrAddAsync<T>(entity, addFunc, updateFunc);

    public async Task DeleteAsync(T entity) =>
        await DeleteAsync<T>(entity);

    public async Task DeleteAsync(Expression<Func<T, bool>> predicate) =>
        await DeleteAsync<T>(predicate);
    
    #endregion



    #region PRIVATE METHODS

    protected async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(Func<IQueryable<TEntity>, IQueryable<TEntity>>? additionalIncludes = null) where TEntity : class => 
        await Database.Set<TEntity>()
                      .Include(additionalIncludes)
                      .ToListAsync();
    
    protected async Task UpdateAsync<TEntity>(TEntity entity, Action<TEntity> updateFunc) where TEntity : class
    { 
        updateFunc(entity);
        Database.Set<TEntity>().Update(entity);
        await Database.SaveChangesAsync();
    }
    
    protected async Task AddAsync<TEntity>(TEntity entity) where TEntity : class
    {
        await Database.Set<TEntity>().AddAsync(entity);
        await Database.SaveChangesAsync();
    }
    
    protected async Task<TEntity> UpdateOrAddAsync<TEntity>(TEntity? entity, Func<TEntity> addFunc, Action<TEntity> updateFunc) where TEntity : class
    {
        if (entity is null)
        {
            entity = addFunc();
            await Database.Set<TEntity>().AddAsync(entity);
        }
        else
        {
            updateFunc(entity);
            Database.Set<TEntity>().Update(entity);
        }
        await Database.SaveChangesAsync();
        return entity;
    }

    protected async Task DeleteAsync<TEntity>(TEntity entity) where TEntity : class
    {
        DbSet<TEntity> dbSet = Database.Set<TEntity>();
        dbSet.Attach(entity);
        dbSet.Remove(entity);
        await Database.SaveChangesAsync();
    }
    
    protected async Task DeleteAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
    {
        DbSet<TEntity> dbSet = Database.Set<TEntity>();
        IQueryable<TEntity> entities = dbSet.Where(predicate);
        dbSet.AttachRange(entities);
        dbSet.RemoveRange(entities);
        await Database.SaveChangesAsync();
    }
    
    #endregion
}