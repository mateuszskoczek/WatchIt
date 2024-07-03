using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using WatchIt.Database.Model.Account;

namespace WatchIt.Common.Model.Accounts;

public class RegisterResponse
{
    #region PROPERTIES

    [JsonPropertyName("id")]
    public required long Id { get; init; }

    [JsonPropertyName("username")]
    public required string Username { get; init; }

    [JsonPropertyName("email")]
    public required string Email { get; init; }

    [JsonPropertyName("creation_date")]
    public required DateTime CreationDate { get; init; }

    #endregion



    #region CONSTRUCTORS

    [SetsRequiredMembers]
    public RegisterResponse(Account account)
    {
        Id = account.Id;
        Username = account.Username;
        Email = account.Email;
        CreationDate = account.CreationDate;
    }

    #endregion
}