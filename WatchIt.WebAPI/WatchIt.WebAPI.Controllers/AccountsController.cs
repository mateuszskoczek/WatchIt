using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WatchIt.Common.Model.Accounts;
using WatchIt.WebAPI.Services.Controllers.Accounts;

namespace WatchIt.WebAPI.Controllers;

[ApiController]
[Route("accounts")]
public class AccountsController(IAccountsControllerService accountsControllerService) : ControllerBase
{
    [HttpPost("register")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(RegisterResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Register([FromBody]RegisterRequest body) => await accountsControllerService.Register(body);
    
    [HttpPost("authenticate")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(AuthenticateResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> Authenticate([FromBody]AuthenticateRequest body) => await accountsControllerService.Authenticate(body);
    
    [HttpPost("authenticate-refresh")]
    [Authorize(AuthenticationSchemes = "refresh")]
    [ProducesResponseType(typeof(AuthenticateResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> AuthenticateRefresh() => await accountsControllerService.AuthenticateRefresh();

    [HttpDelete("logout")]
    [Authorize(AuthenticationSchemes = "refresh")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Logout() => await accountsControllerService.Logout();

    [HttpGet("{id}/profile-picture")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(AccountProfilePictureResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetAccountProfilePicture([FromRoute(Name = "id")]long id) => await accountsControllerService.GetAccountProfilePicture(id);
}