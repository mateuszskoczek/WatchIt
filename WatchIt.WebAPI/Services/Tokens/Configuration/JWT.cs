namespace WatchIt.WebAPI.Services.Tokens.Configuration;

public class JWT
{
    #region PROPERTIES

    public string Key { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public string Algorithm { get; set; } = null!;
    public TokensLifetime Lifetime { get; set; } = null!;

    #endregion
}