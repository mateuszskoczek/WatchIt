using WatchIt.Common.Model.Roles;

namespace WatchIt.Website.Services.WebAPI.Roles;

public interface IRolesWebAPIService
{
    Task GetAllActorRoles(RoleQueryParameters? query = null, Action<IEnumerable<RoleResponse>>? successAction = null);
    Task GetActorRole(long id, Action<RoleResponse>? successAction = null, Action? notFoundAction = null);
    Task PostActorRole(RoleRequest data, Action<RoleResponse>? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null);
    Task DeleteActorRole(long id, Action? successAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null);
    Task GetAllCreatorRoles(RoleQueryParameters? query = null, Action<IEnumerable<RoleResponse>>? successAction = null);
    Task GetCreatorRole(long id, Action<RoleResponse>? successAction = null, Action? notFoundAction = null);
    Task PostCreatorRole(RoleRequest data, Action<RoleResponse>? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null);
    Task DeleteCreatorRole(long id, Action? successAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null);
}