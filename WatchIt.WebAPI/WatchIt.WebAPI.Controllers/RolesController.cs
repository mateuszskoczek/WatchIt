using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WatchIt.Common.Model.Roles;
using WatchIt.WebAPI.Services.Controllers.Roles;

namespace WatchIt.WebAPI.Controllers;

[ApiController]
[Route("roles")]
public class RolesController : ControllerBase
{
    #region SERVICES

    private readonly IRolesControllerService _rolesControllerService;
    
    #endregion
    
    
    
    #region CONSTRUCTORS

    public RolesController(IRolesControllerService rolesControllerService)
    {
        _rolesControllerService = rolesControllerService;
    }
    
    #endregion



    #region METHODS
    
    #region Actor
    
    [HttpGet("actor/{id}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<ActorRoleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetActorRole([FromRoute]Guid id) => await _rolesControllerService.GetActorRole(id);
    
    [HttpPut("actor/{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> PutActorRole([FromRoute]Guid id, [FromBody]ActorRoleRequest body) => await _rolesControllerService.PutActorRole(id, body);
    
    [HttpDelete("actor/{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> DeleteActorRole([FromRoute]Guid id) => await _rolesControllerService.DeleteActorRole(id);
    
    [HttpGet("actor/type")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<RoleTypeResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAllActorRoleTypes(RoleTypeQueryParameters query) => await _rolesControllerService.GetAllActorRoleTypes(query);
    
    [HttpGet("actor/type/{type_id}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(RoleTypeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetActorRoleType([FromRoute(Name = "type_id")]short typeId) => await _rolesControllerService.GetActorRoleType(typeId);
    
    [HttpPost("actor/type")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(RoleTypeResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> PostActorRoleType([FromBody]RoleTypeRequest body) => await _rolesControllerService.PostActorRoleType(body);
    
    [HttpDelete("actor/type/{type_id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> DeleteActorRoleType([FromRoute(Name = "type_id")]short typeId) => await _rolesControllerService.DeleteActorRoleType(typeId);
    
    #endregion
    
    #region Creator
    
    [HttpGet("creator/{id}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<CreatorRoleResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetCreatorRole([FromRoute]Guid id) => await _rolesControllerService.GetCreatorRole(id);
    
    [HttpPut("creator/{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> PutCreatorRole([FromRoute]Guid id, [FromBody]CreatorRoleRequest body) => await _rolesControllerService.PutCreatorRole(id, body);
    
    [HttpDelete("creator/{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> DeleteCreatorRole([FromRoute]Guid id) => await _rolesControllerService.DeleteCreatorRole(id);
    
    [HttpGet("creator/type")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<RoleTypeResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAllCreatorRoleTypes(RoleTypeQueryParameters query) => await _rolesControllerService.GetAllCreatorRoleTypes(query);
    
    [HttpGet("creator/type/{id}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(RoleTypeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetCreatorRoleType([FromRoute]short id) => await _rolesControllerService.GetCreatorRoleType(id);
    
    [HttpPost("creator/type")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(RoleTypeResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> PostCreatorRoleType([FromBody]RoleTypeRequest body) => await _rolesControllerService.PostCreatorRoleType(body);
    
    [HttpDelete("creator/type/{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> DeleteCreatorRoleType([FromRoute]short id) => await _rolesControllerService.DeleteCreatorRoleType(id);
    
    #endregion
    
    #endregion
}