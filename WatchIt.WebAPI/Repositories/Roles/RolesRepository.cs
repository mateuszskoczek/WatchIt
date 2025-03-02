using Microsoft.EntityFrameworkCore;
using WatchIt.Database;
using WatchIt.Database.Model.Roles;
using WatchIt.DTO.Models.Controllers.Roles.Role;
using WatchIt.DTO.Models.Controllers.Roles.Role.Query;
using WatchIt.DTO.Models.Controllers.Roles.RoleActorType;
using WatchIt.DTO.Models.Controllers.Roles.RoleCreatorType;
using WatchIt.DTO.Query;

namespace WatchIt.WebAPI.Repositories.Roles;

public class RolesRepository : Repository<Role>, IRolesRepository
{
    #region CONSTRUCTORS
    
    public RolesRepository(DatabaseContext database) : base(database) {}
    
    #endregion
    
    
    
    #region PUBLIC METHODS

    #region Main

    public async Task<bool> ExistsAsync(Guid id) =>
        await DefaultSet.AnyAsync(x => x.Id == id);

    public async Task<Role?> GetAsync(Guid id, Func<IQueryable<Role>, IQueryable<Role>>? additionalIncludes = null) =>
        await DefaultSet.Include(additionalIncludes)
                        .FirstOrDefaultAsync(x => x.Id == id);
    
    public async Task<T?> GetAsync<T>(Guid id, Func<IQueryable<T>, IQueryable<T>>? additionalIncludes = null) where T : Role =>
        await DefaultSet.OfType<T>()
                        .Include(additionalIncludes)
                        .FirstOrDefaultAsync(x => x.Id == id);
    
    public async Task<IEnumerable<RoleActor>> GetAllActorsAsync(RoleActorFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery, Func<IQueryable<RoleActor>, IQueryable<RoleActor>>? additionalIncludes = null) =>
        await DefaultSet.OfType<RoleActor>()
                        .ApplyFilter(filterQuery)
                        .ApplyOrder(orderQuery, RoleOrderKeys.Base<RoleActor>(), RoleOrderKeys.RoleActor)
                        .ApplyPaging(pagingQuery)
                        .Include(additionalIncludes)
                        .ToListAsync();
    
    public async Task<IEnumerable<RoleCreator>> GetAllCreatorsAsync(RoleCreatorFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery, Func<IQueryable<RoleCreator>, IQueryable<RoleCreator>>? additionalIncludes = null) =>
        await DefaultSet.OfType<RoleCreator>()
                        .ApplyFilter(filterQuery)
                        .ApplyOrder(orderQuery, RoleOrderKeys.Base<RoleCreator>(), RoleOrderKeys.RoleCreator)
                        .ApplyPaging(pagingQuery)
                        .Include(additionalIncludes)
                        .ToListAsync();
    
    public async Task<bool> UpdateAsync<T>(Guid id, Action<T> updateFunc) where T : Role
    {
        T? entity = await GetAsync<T>(id);
        if (entity is null)
        {
            return false;
        }
        
        updateFunc(entity);
        DefaultSet.Update(entity);
        await Database.SaveChangesAsync();
        return true;
    }
    
    public async Task DeleteAsync(Guid id) =>
        await DeleteAsync(x => x.Id == id);
    
    #endregion

    #region Rating

    public async Task<IEnumerable<RoleRating>> GetRoleRatingsAsync(Guid roleId, Func<IQueryable<RoleRating>, IQueryable<RoleRating>>? additionalIncludes = null) =>
        await Database.RoleRatings
                      .Where(x => x.RoleId == roleId)
                      .Include(additionalIncludes)
                      .ToListAsync();

    public async Task<RoleRating?> GetRoleRatingByUserAsync(Guid roleId, long accountId, Func<IQueryable<RoleRating>, IQueryable<RoleRating>>? additionalIncludes = null) =>
        await Database.RoleRatings
                      .Include(additionalIncludes)
                      .FirstOrDefaultAsync(x => x.RoleId == roleId && x.AccountId == accountId);

    public async Task<IEnumerable<RoleRating>> GetPersonRoleRatingsAsync(long personId, Func<IQueryable<RoleRating>, IQueryable<RoleRating>>? additionalIncludes = null) =>
        await Database.RoleRatings
                      .Where(x => x.Role.PersonId == personId)
                      .Include(additionalIncludes)
                      .ToListAsync();
    
    public async Task<IEnumerable<RoleRating>> GetPersonRoleRatingsByAccountIdAsync(long personId, long accountId, Func<IQueryable<RoleRating>, IQueryable<RoleRating>>? additionalIncludes = null) =>
        await Database.RoleRatings
                      .Where(x => x.Role.PersonId == personId && x.AccountId == accountId)
                      .Include(additionalIncludes)
                      .ToListAsync();
    
    public async Task<RoleRating> UpdateOrAddRoleRatingAsync(Guid roleId, long accountId, Func<RoleRating> addFunc, Action<RoleRating> updateFunc) =>
        await UpdateOrAddAsync(await GetRoleRatingByUserAsync(roleId, accountId), addFunc, updateFunc);
    
    public async Task DeleteRoleUserRatingAsync(Guid roleId, long accountId) =>
        await DeleteAsync<RoleRating>(x => x.RoleId == roleId && x.AccountId == accountId);

    #endregion

    #region ActorTypes

    public async Task<bool> ExistsActorTypeAsync(short id) =>
        await Database.RoleActorTypes
                      .AnyAsync(x => x.Id == id);

    public async Task<RoleActorType?> GetActorTypeAsync(short id, Func<IQueryable<RoleActorType>, IQueryable<RoleActorType>>? additionalIncludes = null) =>
        await Database.RoleActorTypes
                      .Include(additionalIncludes)
                      .FirstOrDefaultAsync(x => x.Id == id);
    
    public async Task<IEnumerable<RoleActorType>> GetAllActorTypesAsync(RoleActorTypeFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery, Func<IQueryable<RoleActorType>, IQueryable<RoleActorType>>? additionalIncludes = null) =>
        await Database.RoleActorTypes
                      .ApplyFilter(filterQuery)
                      .ApplyOrder(orderQuery, RoleActorTypeOrderKeys.Base)
                      .ApplyPaging(pagingQuery)
                      .Include(additionalIncludes)
                      .ToListAsync();

    public async Task AddActorTypeAsync(RoleActorType actorType) =>
        await AddAsync(actorType);
    
    public async Task DeleteActorTypeAsync(short id) =>
        await DeleteAsync(new RoleActorType { Id = id });

    #endregion

    #region CreatorTypes

    public async Task<bool> ExistsCreatorTypeAsync(short id) =>
        await Database.RoleCreatorTypes
                      .AnyAsync(x => x.Id == id);

    public async Task<RoleCreatorType?> GetCreatorTypeAsync(short id, Func<IQueryable<RoleCreatorType>, IQueryable<RoleCreatorType>>? additionalIncludes = null) =>
        await Database.RoleCreatorTypes
                      .Include(additionalIncludes)
                      .FirstOrDefaultAsync(x => x.Id == id);
    
    public async Task<IEnumerable<RoleCreatorType>> GetAllCreatorTypesAsync(RoleCreatorTypeFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery, Func<IQueryable<RoleCreatorType>, IQueryable<RoleCreatorType>>? additionalIncludes = null) =>
        await Database.RoleCreatorTypes
                      .ApplyFilter(filterQuery)
                      .ApplyOrder(orderQuery, RoleCreatorTypeOrderKeys.Base)
                      .ApplyPaging(pagingQuery)
                      .Include(additionalIncludes)
                      .ToListAsync();

    public async Task AddCreatorTypeAsync(RoleCreatorType creatorType) =>
        await AddAsync(creatorType);
    
    public async Task DeleteCreatorTypeAsync(short id) =>
        await DeleteAsync(new RoleCreatorType { Id = id });

    #endregion

    #endregion
}