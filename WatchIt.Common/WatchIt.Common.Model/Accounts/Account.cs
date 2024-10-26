using System.Text.Json.Serialization;

namespace WatchIt.Common.Model.Accounts;

public abstract class Account
{
    #region PROPERTIES
    
    [JsonPropertyName("username")]
    public required string Username { get; set; }
    
    [JsonPropertyName("email")]
    public required string Email { get; set; }
    
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    
    #endregion
}