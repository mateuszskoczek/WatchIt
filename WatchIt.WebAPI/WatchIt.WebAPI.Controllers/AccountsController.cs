using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WatchIt.Common.Model.Accounts;
using WatchIt.Common.Model.Movies;
using WatchIt.Common.Model.Persons;
using WatchIt.Common.Model.Photos;
using WatchIt.Common.Model.Series;
using WatchIt.WebAPI.Services.Controllers.Accounts;

namespace WatchIt.WebAPI.Controllers;

[ApiController]
[Route("accounts")]
public class AccountsController(IAccountsControllerService accountsControllerService) : ControllerBase
{
    #region Basic
    
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
    
    [HttpPost("authenticate_refresh")]
    [Authorize(AuthenticationSchemes = "refresh")]
    [ProducesResponseType(typeof(AuthenticateResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> AuthenticateRefresh() => await accountsControllerService.AuthenticateRefresh();

    [HttpDelete("logout")]
    [Authorize(AuthenticationSchemes = "refresh")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Logout() => await accountsControllerService.Logout();

    #endregion
    
    #region Profile picture
    
    [HttpGet("{id}/profile_picture")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(AccountProfilePictureResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetAccountProfilePicture([FromRoute(Name = "id")]long id) => await accountsControllerService.GetAccountProfilePicture(id);
    
    [HttpPut("profile_picture")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(AccountProfilePictureResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> PutAccountProfilePicture([FromBody]AccountProfilePictureRequest body) => await accountsControllerService.PutAccountProfilePicture(body);
    
    [HttpDelete("profile_picture")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> DeleteAccountProfilePicture() => await accountsControllerService.DeleteAccountProfilePicture();
    
    #endregion
    
    #region Profile background
    
    [HttpGet("{id}/profile_background")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(PhotoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetAccountProfileBackground([FromRoute(Name = "id")]long id) => await accountsControllerService.GetAccountProfileBackground(id);
    
    [HttpPut("profile_background")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(PhotoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> PutAccountProfileBackground([FromBody]AccountProfileBackgroundRequest body) => await accountsControllerService.PutAccountProfileBackground(body);
    
    [HttpDelete("profile_background")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> DeleteAccountProfileBackground() => await accountsControllerService.DeleteAccountProfileBackground();
    
    #endregion
    
    #region Info
    
    [HttpGet("{id}/info")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(AccountResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetAccountInfo([FromRoute]long id) => await accountsControllerService.GetAccountInfo(id);
    
    [HttpPut("profile_info")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> PutAccountProfileInfo([FromBody]AccountProfileInfoRequest data) => await accountsControllerService.PutAccountProfileInfo(data);
    
    [HttpPatch("username")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> PatchAccountUsername([FromBody]AccountUsernameRequest data) => await accountsControllerService.PatchAccountUsername(data);
    
    [HttpPatch("email")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> PatchAccountEmail([FromBody]AccountEmailRequest data) => await accountsControllerService.PatchAccountEmail(data);
    
    [HttpPatch("password")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> PatchAccountPassword([FromBody]AccountPasswordRequest data) => await accountsControllerService.PatchAccountPassword(data);
    
    #endregion
    
    [HttpGet("{id}/movies")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<MovieRatedResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetAccountRatedMovies([FromRoute]long id, MovieRatedQueryParameters query) => await accountsControllerService.GetAccountRatedMovies(id, query);
    
    [HttpGet("{id}/series")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<SeriesRatedResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetAccountRatedSeries([FromRoute]long id, SeriesRatedQueryParameters query) => await accountsControllerService.GetAccountRatedSeries(id, query);
    
    [HttpGet("{id}/persons")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<PersonRatedResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetAccountRatedPersons([FromRoute]long id, PersonRatedQueryParameters query) => await accountsControllerService.GetAccountRatedPersons(id, query);
}