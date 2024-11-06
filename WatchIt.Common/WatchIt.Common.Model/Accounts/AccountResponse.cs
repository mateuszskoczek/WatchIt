using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using WatchIt.Common.Model.Genders;
using WatchIt.Common.Query;

namespace WatchIt.Common.Model.Accounts;

public class AccountResponse : Account, IQueryOrderable<AccountResponse>
{
    #region PROPERTIES
    
    [JsonIgnore]
    public static IDictionary<string, Func<AccountResponse, IComparable>> OrderableProperties { get; } = new Dictionary<string, Func<AccountResponse, IComparable>>
    {
        { "id", x => x.Id },
        { "username", x => x.Username },
        { "email", x => x.Email },
        { "description", x => x.Description },
        { "gender", x => x.Gender.Name },
        { "last_active", x => x.LastActive },
        { "creation_date", x => x.CreationDate },
        { "is_admin", x => x.IsAdmin }
    };
    
    
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