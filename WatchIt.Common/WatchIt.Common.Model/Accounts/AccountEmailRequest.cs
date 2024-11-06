using System.Text.Json.Serialization;

namespace WatchIt.Common.Model.Accounts;

public class AccountEmailRequest
{
    #region PROPERTIES
    
    [JsonPropertyName("new_email")]
    public string NewEmail { get; set; }
    
    [JsonPropertyName("password")]
    public string Password { get; set; }
    
    #endregion



    #region PUBLIC METHODS

    public void UpdateAccount(Database.Model.Account.Account account)
    {
        account.Email = NewEmail;
    }

    #endregion
}