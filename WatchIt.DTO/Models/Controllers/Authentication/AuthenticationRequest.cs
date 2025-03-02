namespace WatchIt.DTO.Models.Controllers.Authentication;

public class AuthenticationRequest
{
    #region PROPERTIES

    public string UsernameOrEmail { get; set; } = null!;
    public string Password { get; set; } = null!;
    public bool RememberMe { get; set; }

    #endregion
}