using WatchIt.Common.Model.Roles;

namespace WatchIt.Website.Services.WebAPI.Roles;

public interface IRolesWebAPIService
{
    Task GetActorRole(Guid id, Action<ActorRoleResponse>? successAction = null, Action? notFoundAction = null);
    Task PutActorRole(Guid id, ActorRoleRequest data, Action? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null, Action? notFoundAction = null);
    Task DeleteActorRole(Guid id, Action? successAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null);
    Task GetAllActorRoleTypes(RoleTypeQueryParameters? query = null, Action<IEnumerable<RoleTypeResponse>>? successAction = null);
    Task GetActorRoleType(long typeId, Action<RoleTypeResponse>? successAction = null, Action? notFoundAction = null);
    Task PostActorRoleType(RoleTypeRequest data, Action<RoleTypeResponse>? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null);
    Task DeleteActorRoleType(long typeId, Action? successAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null);
    Task GetCreatorRole(Guid id, Action<CreatorRoleResponse>? successAction = null, Action? notFoundAction = null);
    Task PutCreatorRole(Guid id, CreatorRoleRequest data, Action? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null, Action? notFoundAction = null);
    Task DeleteCreatorRole(Guid id, Action? successAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null);
    Task GetAllCreatorRoleTypes(RoleTypeQueryParameters? query = null, Action<IEnumerable<RoleTypeResponse>>? successAction = null);
    Task GetCreatorRoleType(long typeId, Action<RoleTypeResponse>? successAction = null, Action? notFoundAction = null);
    Task PostCreatorRoleType(RoleTypeRequest data, Action<RoleTypeResponse>? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null);
    Task DeleteCreatorRoleType(long typeId, Action? successAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null);
}