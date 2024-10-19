using WatchIt.Common.Model.Rating;
using WatchIt.Common.Model.Roles;
using WatchIt.WebAPI.Services.Controllers.Common;

namespace WatchIt.WebAPI.Services.Controllers.Roles;

public interface IRolesControllerService
{
    Task<RequestResult> GetActorRole(Guid id);
    Task<RequestResult> PutActorRole(Guid id, ActorRoleUniversalRequest data);
    Task<RequestResult> DeleteActorRole(Guid id);
    Task<RequestResult> GetActorRoleRating(Guid id);
    Task<RequestResult> GetActorRoleRatingByUser(Guid id, long userId);
    Task<RequestResult> PutActorRoleRating(Guid id, RatingRequest data);
    Task<RequestResult> DeleteActorRoleRating(Guid id);
    Task<RequestResult> GetAllActorRoleTypes(RoleTypeQueryParameters query);
    Task<RequestResult> GetActorRoleType(short typeId);
    Task<RequestResult> PostActorRoleType(RoleTypeRequest data);
    Task<RequestResult> DeleteActorRoleType(short typeId);
    
    Task<RequestResult> GetCreatorRole(Guid id);
    Task<RequestResult> PutCreatorRole(Guid id, CreatorRoleUniversalRequest data);
    Task<RequestResult> DeleteCreatorRole(Guid id);
    Task<RequestResult> GetCreatorRoleRating(Guid id);
    Task<RequestResult> GetCreatorRoleRatingByUser(Guid id, long userId);
    Task<RequestResult> PutCreatorRoleRating(Guid id, RatingRequest data);
    Task<RequestResult> DeleteCreatorRoleRating(Guid id);
    Task<RequestResult> GetAllCreatorRoleTypes(RoleTypeQueryParameters query);
    Task<RequestResult> GetCreatorRoleType(short typeId);
    Task<RequestResult> PostCreatorRoleType(RoleTypeRequest data);
    Task<RequestResult> DeleteCreatorRoleType(short typeId);
}