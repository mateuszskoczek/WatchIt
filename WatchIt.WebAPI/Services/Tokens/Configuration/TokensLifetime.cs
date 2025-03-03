namespace WatchIt.WebAPI.Services.Tokens.Configuration;

public class TokensLifetime
{
    #region PROPERTIES

    public TokenLifetime AccessToken { get; set; } = null!;
    public TokenLifetime RefreshToken { get; set; } = null!;

    #endregion
}