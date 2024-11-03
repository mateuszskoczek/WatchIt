using System.Text.Json.Serialization;

namespace WatchIt.Common.Model.Accounts;

public class AccountProfileInfoRequest
{
    #region PROPERTIES
    
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("gender_id")]
    public short? GenderId { get; set; }

    #endregion
    
    
    
    #region CONSTRUCTORS
    
    public AccountProfileInfoRequest() { }
    
    public AccountProfileInfoRequest(AccountResponse accountResponse)
    {
        Description = accountResponse.Description;
        GenderId = accountResponse.Gender?.Id;
    }
    
    #endregion
    
    
    
    #region PUBLIC METHODS
    
    public void UpdateAccount(Database.Model.Account.Account account)
    {
        account.Description = Description;
        account.GenderId = GenderId;
    }
    
    #endregion
}