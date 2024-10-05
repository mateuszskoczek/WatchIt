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
    
    [HttpGet("actor")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<RoleResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAllActorRoles(RoleQueryParameters query) => await _rolesControllerService.GetAllActorRoles(query);
    
    [HttpGet("actor/{id}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(RoleResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetActorRole([FromRoute]short id) => await _rolesControllerService.GetActorRole(id);
    
    [HttpPost("actor")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(RoleResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> PostActorRoles([FromBody]RoleRequest body) => await _rolesControllerService.PostActorRole(body);
    
    [HttpDelete("actor/{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> DeleteActorRoles([FromRoute]short id) => await _rolesControllerService.DeleteActorRole(id);
    
    #endregion
    
    #region Creator
    
    [HttpGet("creator")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<RoleResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAllCreatorRoles(RoleQueryParameters query) => await _rolesControllerService.GetAllCreatorRoles(query);
    
    [HttpGet("creator/{id}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(RoleResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetCreatorRole([FromRoute]short id) => await _rolesControllerService.GetCreatorRole(id);
    
    [HttpPost("creator")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(RoleResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> PostCreatorRoles([FromBody]RoleRequest body) => await _rolesControllerService.PostCreatorRole(body);
    
    [HttpDelete("creator/{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> DeleteCreatorRoles([FromRoute]short id) => await _rolesControllerService.DeleteCreatorRole(id);
    
    #endregion
    
    #endregion
}