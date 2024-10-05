using WatchIt.Common.Model.Roles;
using WatchIt.WebAPI.Services.Controllers.Common;

namespace WatchIt.WebAPI.Services.Controllers.Roles;

public interface IRolesControllerService
{
    Task<RequestResult> GetAllActorRoles(RoleQueryParameters query);
    Task<RequestResult> GetActorRole(short id);
    Task<RequestResult> PostActorRole(RoleRequest data);
    Task<RequestResult> DeleteActorRole(short id);
    
    Task<RequestResult> GetAllCreatorRoles(RoleQueryParameters query);
    Task<RequestResult> GetCreatorRole(short id);
    Task<RequestResult> PostCreatorRole(RoleRequest data);
    Task<RequestResult> DeleteCreatorRole(short id);
}