using System.Text.Json.Serialization;

namespace WatchIt.Common.Model.Accounts;

public class AuthenticateResponse
{
    #region PROPERTIES

    [JsonPropertyName("access_token")]
    public required string AccessToken { get; init; }

    [JsonPropertyName("refresh_token")]
    public required string RefreshToken { get; init; }

    #endregion
}