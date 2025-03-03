using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WatchIt.DTO.Models.Controllers.Accounts.Account;
using WatchIt.DTO.Models.Controllers.Authentication;
using WatchIt.WebAPI.BusinessLogic.Authentication;

namespace WatchIt.WebAPI.Controllers;

[ApiController]
[Route("authentication")]
public class AuthenticationController(IAuthenticationBusinessLogic authenticationBusinessLogic) : ControllerBase
{
    [HttpPost("authenticate")]
    [AllowAnonymous]
    [TranslateResultToActionResult]
    public async Task<Result<AuthenticationResponse>> Authenticate([FromBody]AuthenticationRequest body) => await authenticationBusinessLogic.Authenticate(body);
    
    [HttpPost("authenticate_refresh")]
    [AllowAnonymous]
    [TranslateResultToActionResult]
    public async Task<Result<AuthenticationResponse>> AuthenticateRefresh([FromBody]AuthenticationRefreshRequest body) => await authenticationBusinessLogic.AuthenticateRefresh(body);
}