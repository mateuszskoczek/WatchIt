using System.Text.Json.Serialization;

namespace WatchIt.Common.Model.Accounts;

public class AccountRequest : Account
{
    #region PROPERTIES

    [JsonPropertyName("gender_id")]
    public short? GenderId { get; set; }

    #endregion
    
    
    
    #region PUBLIC METHODS
    
    public void UpdateAccount(Database.Model.Account.Account account)
    {
        account.Username = Username;
        account.Email = Email;
        account.Description = Description;
        account.GenderId = GenderId;
    }
    
    #endregion
}