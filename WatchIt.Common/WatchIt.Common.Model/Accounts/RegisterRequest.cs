using System.Text.Json.Serialization;

namespace WatchIt.Common.Model.Accounts;

public class RegisterRequest
{
    [JsonPropertyName("username")]
    public required string Username { get; set; }

    [JsonPropertyName("email")]
    public required string Email { get; set; }

    [JsonPropertyName("password")]
    public required string Password { get; set; }
}