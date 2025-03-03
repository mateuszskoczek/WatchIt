using System.Linq.Expressions;
using WatchIt.Database.Model.Roles;
using WatchIt.DTO.Models.Controllers.Roles.Role.Query;
using WatchIt.DTO.Models.Controllers.Roles.RoleActorType;
using WatchIt.DTO.Models.Controllers.Roles.RoleCreatorType;
using WatchIt.DTO.Query;

namespace WatchIt.WebAPI.Repositories.Roles;

public interface IRolesRepository : IRepository<Role>
{
    Task<bool> ExistsAsync(Guid id);
    Task<Role?> GetAsync(Guid id, Func<IQueryable<Role>, IQueryable<Role>>? additionalIncludes = null);
    Task<T?> GetAsync<T>(Guid id, Func<IQueryable<T>, IQueryable<T>>? additionalIncludes = null) where T : Role;
    Task<IEnumerable<RoleActor>> GetAllActorsAsync(RoleActorFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery, Func<IQueryable<RoleActor>, IQueryable<RoleActor>>? additionalIncludes = null);
    Task<IEnumerable<RoleCreator>> GetAllCreatorsAsync(RoleCreatorFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery, Func<IQueryable<RoleCreator>, IQueryable<RoleCreator>>? additionalIncludes = null);
    Task<bool> UpdateAsync<T>(Guid id, Action<T> updateFunc) where T : Role;
    Task DeleteAsync(Guid id);
    
    Task<IEnumerable<RoleRating>> GetRoleRatingsAsync(Guid roleId, Func<IQueryable<RoleRating>, IQueryable<RoleRating>>? additionalIncludes = null);
    Task<RoleRating?> GetRoleRatingByUserAsync(Guid roleId, long accountId, Func<IQueryable<RoleRating>, IQueryable<RoleRating>>? additionalIncludes = null);
    Task<IEnumerable<RoleRating>> GetPersonRoleRatingsAsync(long personId, Func<IQueryable<RoleRating>, IQueryable<RoleRating>>? additionalIncludes = null);
    Task<IEnumerable<RoleRating>> GetPersonRoleRatingsByAccountIdAsync(long personId, long accountId, Func<IQueryable<RoleRating>, IQueryable<RoleRating>>? additionalIncludes = null);
    Task<RoleRating> UpdateOrAddRoleRatingAsync(Guid roleId, long accountId, Func<RoleRating> addFunc, Action<RoleRating> updateFunc);
    Task DeleteRoleUserRatingAsync(Guid roleId, long accountId);
    
    Task<bool> ExistsActorTypeAsync(short id);
    Task<RoleActorType?> GetActorTypeAsync(short id, Func<IQueryable<RoleActorType>, IQueryable<RoleActorType>>? additionalIncludes = null);
    Task<IEnumerable<RoleActorType>> GetAllActorTypesAsync(RoleActorTypeFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery, Func<IQueryable<RoleActorType>, IQueryable<RoleActorType>>? additionalIncludes = null);
    Task AddActorTypeAsync(RoleActorType actorType);
    Task DeleteActorTypeAsync(short id);
    
    Task<bool> ExistsCreatorTypeAsync(short id);
    Task<RoleCreatorType?> GetCreatorTypeAsync(short id, Func<IQueryable<RoleCreatorType>, IQueryable<RoleCreatorType>>? additionalIncludes = null);
    Task<IEnumerable<RoleCreatorType>> GetAllCreatorTypesAsync(RoleCreatorTypeFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery, Func<IQueryable<RoleCreatorType>, IQueryable<RoleCreatorType>>? additionalIncludes = null);
    Task AddCreatorTypeAsync(RoleCreatorType creatorType);
    Task DeleteCreatorTypeAsync(short id);
}