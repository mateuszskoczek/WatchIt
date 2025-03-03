using Ardalis.Result;
using WatchIt.DTO.Models.Controllers.Accounts.Account;
using WatchIt.DTO.Models.Controllers.Authentication;

namespace WatchIt.WebAPI.BusinessLogic.Authentication;

public interface IAuthenticationBusinessLogic
{
    Task<Result<AuthenticationResponse>> Authenticate(AuthenticationRequest body);
    Task<Result<AuthenticationResponse>> AuthenticateRefresh(AuthenticationRefreshRequest body);
}