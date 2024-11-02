using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using WatchIt.Common.Model.Genders;

namespace WatchIt.Common.Model.Accounts;

public class AccountResponse : Account
{
    #region PROPERTIES
    
    [JsonPropertyName("id")]
    public required long Id { get; set; }
    
    [JsonPropertyName("gender")]
    public GenderResponse? Gender { get; set; }
    
    [JsonPropertyName("last_active")]
    public DateTime LastActive { get; set; }
    
    [JsonPropertyName("creation_date")]
    public DateTime CreationDate { get; set; }
    
    [JsonPropertyName("is_admin")]
    public bool IsAdmin { get; set; }
    
    #endregion



    #region CONSTRUCTORS
    
    [JsonConstructor]
    public AccountResponse() {}

    [SetsRequiredMembers]
    public AccountResponse(Database.Model.Account.Account account)
    {
        Id = account.Id;
        Username = account.Username;
        Email = account.Email;
        Description = account.Description;
        Gender = account.Gender is not null ? new GenderResponse(account.Gender) : null;
        LastActive = account.LastActive;
        CreationDate = account.CreationDate;
        IsAdmin = account.IsAdmin;
    }

    #endregion
}