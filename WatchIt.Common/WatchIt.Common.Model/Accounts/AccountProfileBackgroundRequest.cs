using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace WatchIt.Common.Model.Accounts;

public class AccountProfileBackgroundRequest
{
    #region PROPERTIES
    
    [JsonPropertyName("id")]
    public required Guid Id { get; set; }
    
    #endregion



    #region CONSTRUCTORS

    [SetsRequiredMembers]
    public AccountProfileBackgroundRequest(Guid id)
    {
        Id = id;
    }

    #endregion
}