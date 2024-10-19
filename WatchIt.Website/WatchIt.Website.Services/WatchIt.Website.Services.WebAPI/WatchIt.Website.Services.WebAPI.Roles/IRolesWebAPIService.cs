using WatchIt.Common.Model.Rating;
using WatchIt.Common.Model.Roles;

namespace WatchIt.Website.Services.WebAPI.Roles;

public interface IRolesWebAPIService
{
    Task GetActorRole(Guid id, Action<ActorRoleResponse>? successAction = null, Action? notFoundAction = null);
    Task PutActorRole(Guid id, ActorRoleUniversalRequest data, Action? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null, Action? notFoundAction = null);
    Task DeleteActorRole(Guid id, Action? successAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null);
    Task GetActorRoleRating(Guid id, Action<RatingResponse>? successAction = null, Action? notFoundAction = null);
    Task GetActorRoleRatingByUser(Guid id, long userId, Action<short>? successAction = null, Action? notFoundAction = null);
    Task PutActorRoleRating(Guid id, RatingRequest body, Action? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null, Action? notFoundAction = null);
    Task DeleteActorRoleRating(Guid id, Action? successAction = null, Action? unauthorizedAction = null);
    Task GetAllActorRoleTypes(RoleTypeQueryParameters? query = null, Action<IEnumerable<RoleTypeResponse>>? successAction = null);
    Task GetActorRoleType(long typeId, Action<RoleTypeResponse>? successAction = null, Action? notFoundAction = null);
    Task PostActorRoleType(RoleTypeRequest data, Action<RoleTypeResponse>? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null);
    Task DeleteActorRoleType(long typeId, Action? successAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null);
    
    Task GetCreatorRole(Guid id, Action<CreatorRoleResponse>? successAction = null, Action? notFoundAction = null);
    Task PutCreatorRole(Guid id, CreatorRoleUniversalRequest data, Action? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null, Action? notFoundAction = null);
    Task DeleteCreatorRole(Guid id, Action? successAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null);
    Task GetCreatorRoleRating(Guid id, Action<RatingResponse>? successAction = null, Action? notFoundAction = null);
    Task GetCreatorRoleRatingByUser(Guid id, long userId, Action<short>? successAction = null, Action? notFoundAction = null);
    Task PutCreatorRoleRating(Guid id, RatingRequest body, Action? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null, Action? notFoundAction = null);
    Task DeleteCreatorRoleRating(Guid id, Action? successAction = null, Action? unauthorizedAction = null);
    Task GetAllCreatorRoleTypes(RoleTypeQueryParameters? query = null, Action<IEnumerable<RoleTypeResponse>>? successAction = null);
    Task GetCreatorRoleType(long typeId, Action<RoleTypeResponse>? successAction = null, Action? notFoundAction = null);
    Task PostCreatorRoleType(RoleTypeRequest data, Action<RoleTypeResponse>? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null);
    Task DeleteCreatorRoleType(long typeId, Action? successAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null);
}