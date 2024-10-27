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
    
    #endregion



    #region CONSTRUCTORS

    [SetsRequiredMembers]
    public AccountResponse(Database.Model.Account.Account account)
    {
        Id = account.Id;
        Username = account.Username;
        Email = account.Email;
        Description = account.Description;
        Gender = account.Gender is not null ? new GenderResponse(account.Gender) : null;
    }

    #endregion
}