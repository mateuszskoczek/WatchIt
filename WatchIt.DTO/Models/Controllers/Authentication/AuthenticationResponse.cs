namespace WatchIt.DTO.Models.Controllers.Authentication;

public class AuthenticationResponse
{
    #region PROPERTIES

    public string AccessToken { get; init; } = null!;
    public string RefreshToken { get; init; } = null!;

    #endregion
}