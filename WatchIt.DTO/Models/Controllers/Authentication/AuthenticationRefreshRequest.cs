namespace WatchIt.DTO.Models.Controllers.Authentication;

public class AuthenticationRefreshRequest
{
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
}