using Refit;
using WatchIt.DTO.Models.Controllers.Roles.Role.Query;
using WatchIt.DTO.Models.Controllers.Roles.Role.Request;
using WatchIt.DTO.Models.Controllers.Roles.Role.Response;
using WatchIt.DTO.Models.Controllers.Roles.RoleActorType;
using WatchIt.DTO.Models.Controllers.Roles.RoleCreatorType;
using WatchIt.DTO.Models.Generics.Rating;
using WatchIt.DTO.Query;

namespace WatchIt.Website.Clients;

public interface IRolesClient
{
    #region Main - CRUD

    [Get("/actors")]
    Task<IApiResponse<IEnumerable<RoleActorResponse>>> GetRoleActors([Query] RoleActorFilterQuery? filterQuery = null, [Query] OrderQuery? orderQuery = null, [Query] PagingQuery? pagingQuery = null);

    [Get("/actors/{id}")]
    Task<IApiResponse<RoleActorResponse>> GetRoleActor(Guid id);

    [Get("/creators")]
    Task<IApiResponse<IEnumerable<RoleCreatorResponse>>> GetRoleCreators([Query] RoleCreatorFilterQuery? filterQuery = null, [Query] OrderQuery? orderQuery = null, [Query] PagingQuery? pagingQuery = null);

    [Get("/creators/{id}")]
    Task<IApiResponse<RoleCreatorResponse>> GetRoleCreator(Guid id);

    [Post("/actors")]
    Task<IApiResponse<RoleActorResponse>> PostRoleActor([Authorize]string token, [Body] RoleActorRequest body);

    [Post("/creators")]
    Task<IApiResponse<RoleCreatorResponse>> PostRoleCreator([Authorize]string token, [Body] RoleCreatorRequest body);

    [Put("/actors/{id}")]
    Task<IApiResponse<RoleActorResponse>> PutRoleActor([Authorize]string token, Guid id, [Body] RoleActorRequest body);

    [Put("/creators/{id}")]
    Task<IApiResponse<RoleCreatorResponse>> PutRoleCreator([Authorize]string token, Guid id, [Body] RoleCreatorRequest body);

    [Delete("/{id}")]
    Task<IApiResponse> DeleteRole([Authorize]string token, Guid id);

    #endregion

    #region Main - Rating

    [Get("/{id}/rating")]
    Task<IApiResponse<RatingOverallResponse>> GetRoleRating(Guid id);

    [Get("/{id}/rating/{account_id}")]
    Task<IApiResponse<RatingUserResponse>> GetRoleUserRating(Guid id, [AliasAs("account_id")] long accountId);

    [Put("/{id}/rating")]
    Task<IApiResponse> PutRoleRating([Authorize]string token, Guid id, [Body] RatingRequest body);

    [Delete("/{id}/rating")]
    Task<IApiResponse> DeleteRoleRating([Authorize]string token, Guid id);

    #endregion

    #region ActorTypes

    [Get("/actors/types")]
    Task<IApiResponse<IEnumerable<RoleActorTypeResponse>>> GetRoleActorTypes([Query] RoleActorTypeFilterQuery? filterQuery = null, [Query] OrderQuery? orderQuery = null, [Query] PagingQuery? pagingQuery = null);

    [Get("/actors/types/{role_actor_type_id}")]
    Task<IApiResponse<RoleActorTypeResponse>> GetRoleActorType([AliasAs("role_actor_type_id")] short roleActorTypeId);

    [Post("/actors/types")]
    Task<IApiResponse<RoleActorTypeResponse>> PostRoleActorType([Authorize]string token, [Body] RoleActorTypeRequest body);

    [Delete("/actors/types/{role_actor_type_id}")]
    Task<IApiResponse> DeleteRoleActorType([Authorize]string token, [AliasAs("role_actor_type_id")] short roleActorTypeId);

    #endregion

    #region CreatorTypes

    [Get("/creators/types")]
    Task<IApiResponse<IEnumerable<RoleCreatorTypeResponse>>> GetRoleCreatorTypes([Query] RoleCreatorTypeFilterQuery? filterQuery = null, [Query] OrderQuery? orderQuery = null, [Query] PagingQuery? pagingQuery = null);

    [Get("/creators/types/{role_creator_type_id}")]
    Task<IApiResponse<RoleCreatorTypeResponse>> GetRoleCreatorType([AliasAs("role_creator_type_id")] short roleCreatorTypeId);

    [Post("/creators/types")]
    Task<IApiResponse<RoleCreatorTypeResponse>> PostRoleCreatorType([Authorize]string token, [Body] RoleCreatorTypeRequest body);

    [Delete("/creators/types/{role_creator_type_id}")]
    Task<IApiResponse> DeleteRoleCreatorType([Authorize]string token, [AliasAs("role_creator_type_id")] short roleCreatorTypeId);

    #endregion
}
