using Ardalis.Result;
using WatchIt.DTO.Models.Controllers.Roles.Role.Query;
using WatchIt.DTO.Models.Controllers.Roles.Role.Request;
using WatchIt.DTO.Models.Controllers.Roles.Role.Response;
using WatchIt.DTO.Models.Controllers.Roles.RoleActorType;
using WatchIt.DTO.Models.Controllers.Roles.RoleCreatorType;
using WatchIt.DTO.Models.Generics.Rating;
using WatchIt.DTO.Query;

namespace WatchIt.WebAPI.BusinessLogic.Roles;

public interface IRolesBusinessLogic
{
    Task<Result<IEnumerable<RoleActorResponse>>> GetRoleActors(RoleActorFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery);
    Task<Result<RoleActorResponse>> GetRoleActor(Guid roleId);
    Task<Result<IEnumerable<RoleCreatorResponse>>> GetRoleCreators(RoleCreatorFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery);
    Task<Result<RoleCreatorResponse>> GetRoleCreator(Guid roleId);
    Task<Result<RoleActorResponse>> PostRoleActor(RoleActorRequest body);
    Task<Result<RoleCreatorResponse>> PostRoleCreator(RoleCreatorRequest body);
    Task<Result<RoleActorResponse>> PutRoleActor(Guid roleId, RoleActorRequest body);
    Task<Result<RoleCreatorResponse>> PutRoleCreator(Guid roleId, RoleCreatorRequest body);
    Task<Result> DeleteRole(Guid roleId);
    
    Task<Result<RatingOverallResponse>> GetRoleRating(Guid roleId);
    Task<Result<RatingUserResponse>> GetRoleUserRating(Guid roleId, long accountId);
    Task<Result> PutRoleRating(Guid roleId, RatingRequest body);
    Task<Result> DeleteRoleRating(Guid roleId);
    
    Task<Result<IEnumerable<RoleActorTypeResponse>>> GetRoleActorTypes(RoleActorTypeFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery);
    Task<Result<RoleActorTypeResponse>> GetRoleActorType(short roleActorTypeId);
    Task<Result<RoleActorTypeResponse>> PostRoleActorType(RoleActorTypeRequest body);
    Task<Result> DeleteRoleActorType(short roleActorTypeId);
    Task<Result<IEnumerable<RoleCreatorTypeResponse>>> GetRoleCreatorTypes(RoleCreatorTypeFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery);
    Task<Result<RoleCreatorTypeResponse>> GetRoleCreatorType(short roleCreatorTypeId);
    Task<Result<RoleCreatorTypeResponse>> PostRoleCreatorType(RoleCreatorTypeRequest body);
    Task<Result> DeleteRoleCreatorType(short roleCreatorTypeId);
}