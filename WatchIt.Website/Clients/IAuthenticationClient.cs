using Refit;
using WatchIt.DTO.Models.Controllers.Authentication;

namespace WatchIt.Website.Clients;

public interface IAuthenticationClient
{
    [Post("/authenticate")]
    Task<IApiResponse<AuthenticationResponse>> Authenticate([Body] AuthenticationRequest body);

    [Post("/authenticate_refresh")]
    Task<IApiResponse<AuthenticationResponse>> AuthenticateRefresh([Body] AuthenticationRefreshRequest body);
}