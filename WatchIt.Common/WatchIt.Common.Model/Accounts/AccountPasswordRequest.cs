using System.Text.Json.Serialization;

namespace WatchIt.Common.Model.Accounts;

public class AccountPasswordRequest
{
    #region PROPERTIES
    
    [JsonPropertyName("old_password")]
    public string OldPassword { get; set; }
    
    [JsonPropertyName("new_password")]
    public string NewPassword { get; set; }
    
    [JsonPropertyName("new_password_confirmation")]
    public string NewPasswordConfirmation { get; set; }
    
    #endregion
}