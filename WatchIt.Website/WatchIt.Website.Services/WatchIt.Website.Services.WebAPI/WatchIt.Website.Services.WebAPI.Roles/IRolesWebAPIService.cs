using WatchIt.Common.Model.Roles;

namespace WatchIt.Website.Services.WebAPI.Roles;

public interface IRolesWebAPIService
{
    Task GetAllActorRoleTypes(RoleTypeQueryParameters? query = null, Action<IEnumerable<RoleTypeResponse>>? successAction = null);
    Task GetActorRoleType(long id, Action<RoleTypeResponse>? successAction = null, Action? notFoundAction = null);
    Task PostActorRoleType(RoleTypeRequest data, Action<RoleTypeResponse>? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null);
    Task DeleteActorRoleType(long id, Action? successAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null);
    Task GetAllCreatorRoleTypes(RoleTypeQueryParameters? query = null, Action<IEnumerable<RoleTypeResponse>>? successAction = null);
    Task GetCreatorRoleType(long id, Action<RoleTypeResponse>? successAction = null, Action? notFoundAction = null);
    Task PostCreatorRoleType(RoleTypeRequest data, Action<RoleTypeResponse>? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null);
    Task DeleteCreatorRoleType(long id, Action? successAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null);
}