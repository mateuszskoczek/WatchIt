using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WatchIt.DTO.Models.Controllers.Roles.Role.Query;
using WatchIt.DTO.Models.Controllers.Roles.Role.Request;
using WatchIt.DTO.Models.Controllers.Roles.Role.Response;
using WatchIt.DTO.Models.Controllers.Roles.RoleActorType;
using WatchIt.DTO.Models.Controllers.Roles.RoleCreatorType;
using WatchIt.DTO.Models.Generics.Rating;
using WatchIt.DTO.Query;
using WatchIt.WebAPI.BusinessLogic.Roles;
using WatchIt.WebAPI.Constants;

namespace WatchIt.WebAPI.Controllers;

[ApiController]
[Route("roles")]
public class RolesController(IRolesBusinessLogic rolesBusinessLogic) : ControllerBase
{
    #region Main - CRUD

    [HttpGet("actors")]
    [AllowAnonymous]
    [TranslateResultToActionResult]
    public async Task<Result<IEnumerable<RoleActorResponse>>> GetRoleActors([FromQuery] RoleActorFilterQuery filterQuery, [FromQuery] OrderQuery orderQuery, [FromQuery] PagingQuery pagingQuery) => 
        await rolesBusinessLogic.GetRoleActors(filterQuery, orderQuery, pagingQuery);

    [HttpGet("actors/{id:guid}")]
    [AllowAnonymous]
    [TranslateResultToActionResult]
    public async Task<Result<RoleActorResponse>> GetRoleActor([FromRoute] Guid id) => 
        await rolesBusinessLogic.GetRoleActor(id);

    [HttpGet("creators")]
    [AllowAnonymous]
    [TranslateResultToActionResult]
    public async Task<Result<IEnumerable<RoleCreatorResponse>>> GetRoleCreators([FromQuery] RoleCreatorFilterQuery filterQuery, [FromQuery] OrderQuery orderQuery, [FromQuery] PagingQuery pagingQuery) => 
        await rolesBusinessLogic.GetRoleCreators(filterQuery, orderQuery, pagingQuery);

    [HttpGet("creators/{id:guid}")]
    [AllowAnonymous]
    [TranslateResultToActionResult]
    public async Task<Result<RoleCreatorResponse>> GetRoleCreator([FromRoute] Guid id) => 
        await rolesBusinessLogic.GetRoleCreator(id);

    [HttpPost("actors")]
    [Authorize(Policy = Policies.Admin)]
    [TranslateResultToActionResult]
    public async Task<Result<RoleActorResponse>> PostRoleActor([FromBody] RoleActorRequest body) => 
        await rolesBusinessLogic.PostRoleActor(body);

    [HttpPost("creators")]
    [Authorize(Policy = Policies.Admin)]
    [TranslateResultToActionResult]
    public async Task<Result<RoleCreatorResponse>> PostRoleCreator([FromBody] RoleCreatorRequest body) => 
        await rolesBusinessLogic.PostRoleCreator(body);

    [HttpPut("actors/{id:guid}")]
    [Authorize(Policy = Policies.Admin)]
    [TranslateResultToActionResult]
    public async Task<Result<RoleActorResponse>> PutRoleActor([FromRoute] Guid id, [FromBody] RoleActorRequest body) => 
        await rolesBusinessLogic.PutRoleActor(id, body);

    [HttpPut("creators/{id:guid}")]
    [Authorize(Policy = Policies.Admin)]
    [TranslateResultToActionResult]
    public async Task<Result<RoleCreatorResponse>> PutRoleCreator([FromRoute] Guid id, [FromBody] RoleCreatorRequest body) => 
        await rolesBusinessLogic.PutRoleCreator(id, body);

    [HttpDelete("{id:guid}")]
    [Authorize(Policy = Policies.Admin)]
    [TranslateResultToActionResult]
    public async Task<Result> DeleteRole([FromRoute] Guid id) => 
        await rolesBusinessLogic.DeleteRole(id);

    #endregion

    #region Main - Rating

    [HttpGet("{id:guid}/rating")]
    [AllowAnonymous]
    [TranslateResultToActionResult]
    public async Task<Result<RatingOverallResponse>> GetRoleRating([FromRoute] Guid id) =>
        await rolesBusinessLogic.GetRoleRating(id);

    [HttpGet("{id:guid}/rating/{account_id:long}")]
    [AllowAnonymous]
    [TranslateResultToActionResult]
    public async Task<Result<RatingUserResponse>> GetRoleUserRating([FromRoute] Guid id, [FromRoute(Name = "account_id")] long accountId) => 
        await rolesBusinessLogic.GetRoleUserRating(id, accountId);

    [HttpPut("{id:guid}/rating")]
    [Authorize]
    [TranslateResultToActionResult]
    public async Task<Result> PutRoleRating([FromRoute] Guid id, [FromBody] RatingRequest body) => 
        await rolesBusinessLogic.PutRoleRating(id, body);

    [HttpDelete("{id:guid}/rating")]
    [Authorize]
    [TranslateResultToActionResult]
    public async Task<Result> DeleteRoleRating([FromRoute] Guid id) => 
        await rolesBusinessLogic.DeleteRoleRating(id);

    #endregion

    #region ActorTypes

    [HttpGet("actors/types")]
    [AllowAnonymous]
    [TranslateResultToActionResult]
    public async Task<Result<IEnumerable<RoleActorTypeResponse>>> GetRoleActorTypes([FromQuery] RoleActorTypeFilterQuery filterQuery, [FromQuery] OrderQuery orderQuery, [FromQuery] PagingQuery pagingQuery) => 
        await rolesBusinessLogic.GetRoleActorTypes(filterQuery, orderQuery, pagingQuery);

    [HttpGet("actors/types/{role_actor_type_id}")]
    [AllowAnonymous]
    [TranslateResultToActionResult]
    public async Task<Result<RoleActorTypeResponse>> GetRoleActorType([FromRoute(Name = "role_actor_type_id")] short roleActorTypeId) => 
        await rolesBusinessLogic.GetRoleActorType(roleActorTypeId);

    [HttpPost("actors/types")]
    [Authorize(Policy = Policies.Admin)]
    [TranslateResultToActionResult]
    public async Task<Result<RoleActorTypeResponse>> PostRoleActorType([FromBody] RoleActorTypeRequest body) => 
        await rolesBusinessLogic.PostRoleActorType(body);

    [HttpDelete("actors/types/{role_actor_type_id}")]
    [Authorize(Policy = Policies.Admin)]
    [TranslateResultToActionResult]
    public async Task<Result> DeleteRoleActorType([FromRoute(Name = "role_actor_type_id")] short roleActorTypeId) => 
        await rolesBusinessLogic.DeleteRoleActorType(roleActorTypeId);

    #endregion

    #region CreatorTypes

    [HttpGet("creators/types")]
    [AllowAnonymous]
    [TranslateResultToActionResult]
    public async Task<Result<IEnumerable<RoleCreatorTypeResponse>>> GetRoleCreatorTypes([FromQuery] RoleCreatorTypeFilterQuery filterQuery, [FromQuery] OrderQuery orderQuery, [FromQuery] PagingQuery pagingQuery) => 
        await rolesBusinessLogic.GetRoleCreatorTypes(filterQuery, orderQuery, pagingQuery);

    [HttpGet("creators/types/{role_creator_type_id}")]
    [AllowAnonymous]
    [TranslateResultToActionResult]
    public async Task<Result<RoleCreatorTypeResponse>> GetRoleCreatorType([FromRoute(Name = "role_creator_type_id")] short roleCreatorTypeId) => 
        await rolesBusinessLogic.GetRoleCreatorType(roleCreatorTypeId);

    [HttpPost("creators/types")]
    [Authorize(Policy = Policies.Admin)]
    [TranslateResultToActionResult]
    public async Task<Result<RoleCreatorTypeResponse>> PostRoleCreatorType([FromBody] RoleCreatorTypeRequest body) => 
        await rolesBusinessLogic.PostRoleCreatorType(body);

    [HttpDelete("creators/types/{role_creator_type_id}")]
    [Authorize(Policy = Policies.Admin)]
    [TranslateResultToActionResult]
    public async Task<Result> DeleteRoleCreatorType([FromRoute(Name = "role_creator_type_id")] short roleCreatorTypeId) => 
        await rolesBusinessLogic.DeleteRoleCreatorType(roleCreatorTypeId);

    #endregion
}
