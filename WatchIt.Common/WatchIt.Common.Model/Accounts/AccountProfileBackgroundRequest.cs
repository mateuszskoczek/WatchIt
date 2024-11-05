using System.Diagnostics.CodeAnalysis;

namespace WatchIt.Common.Model.Accounts;

public class AccountProfileBackgroundRequest
{
    #region PROPERTIES
    
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