using System.Text.Json.Serialization;

namespace WatchIt.Common.Model.Accounts;

public class AccountUsernameRequest
{
    #region PROPERTIES
    
    [JsonPropertyName("new_username")]
    public string NewUsername { get; set; }
    
    [JsonPropertyName("password")]
    public string Password { get; set; }
    
    #endregion



    #region PUBLIC METHODS

    public void UpdateAccount(Database.Model.Account.Account account)
    {
        account.Username = NewUsername;
    }

    #endregion
}