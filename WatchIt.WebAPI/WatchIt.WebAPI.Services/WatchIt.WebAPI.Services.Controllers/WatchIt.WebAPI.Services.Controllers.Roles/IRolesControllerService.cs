using WatchIt.Common.Model.Roles;
using WatchIt.WebAPI.Services.Controllers.Common;

namespace WatchIt.WebAPI.Services.Controllers.Roles;

public interface IRolesControllerService
{
    Task<RequestResult> GetAllActorRoleTypes(RoleTypeQueryParameters query);
    Task<RequestResult> GetActorRoleType(short id);
    Task<RequestResult> PostActorRoleType(RoleTypeRequest data);
    Task<RequestResult> DeleteActorRoleType(short id);
    
    Task<RequestResult> GetAllCreatorRoleTypes(RoleTypeQueryParameters query);
    Task<RequestResult> GetCreatorRoleType(short id);
    Task<RequestResult> PostCreatorRoleType(RoleTypeRequest data);
    Task<RequestResult> DeleteCreatorRoleType(short id);
}