using WatchIt.Database.Model.Accounts;

namespace WatchIt.DTO.Models.Controllers.Authentication;

public static class AuthenticationMappers
{
    #region Authentication

    public static AuthenticationResponse CreateAuthenticationResponse(string accessToken, string refreshToken) => new AuthenticationResponse
    {
        AccessToken = accessToken,
        RefreshToken = refreshToken,
    };

    #endregion
}