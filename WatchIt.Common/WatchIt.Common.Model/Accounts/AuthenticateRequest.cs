using System.Text.Json.Serialization;

namespace WatchIt.Common.Model.Accounts;

public class AuthenticateRequest
{
    #region PROPERTIES

    [JsonPropertyName("username_or_email")]
    public required string UsernameOrEmail { get; set; }

    [JsonPropertyName("password")]
    public required string Password { get; set; }

    [JsonPropertyName("remember_me")]
    public bool RememberMe { get; set; }

    #endregion
}