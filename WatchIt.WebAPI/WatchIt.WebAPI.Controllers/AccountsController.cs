using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WatchIt.Shared.Models.Accounts.Authenticate;
using WatchIt.Shared.Models.Accounts.Register;
using WatchIt.WebAPI.Services.Controllers;

namespace WatchIt.WebAPI.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountsController(IAccountsControllerService accountsControllerService) : ControllerBase
    {
        #region METHODS

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(RegisterResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Register([FromBody] RegisterRequest data) => await accountsControllerService.Register(data);

        [HttpPost]
        [Route("authenticate")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AuthenticateResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Authenticate([FromBody] AuthenticateRequest data) => await accountsControllerService.Authenticate(data);

        [HttpPost]
        [Route("authenticate-refresh")]
        [Authorize(AuthenticationSchemes = "refresh")]
        [ProducesResponseType(typeof(AuthenticateResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> AuthenticateRefresh() => await accountsControllerService.AuthenticateRefresh();


        #endregion
    }
}
