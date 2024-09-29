using System.Text.Json.Serialization;

namespace WatchIt.Common.Model.Accounts;

public abstract class AccountProfilePicture : Picture
{
    #region CONSTRUCTORS

    [JsonConstructor]
    public AccountProfilePicture() {}

    #endregion
}