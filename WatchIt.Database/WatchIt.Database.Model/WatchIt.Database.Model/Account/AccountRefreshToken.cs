namespace WatchIt.Database.Model.Account;

public class AccountRefreshToken
{
    #region PROPERTIES

    public Guid Id { get; set; }
    public required long AccountId { get; set; }
    public required DateTime ExpirationDate { get; set; }
    public required bool IsExtendable { get; set; }

    #endregion



    #region NAVIGATION

    public virtual Account Account { get; set; } = null!;

    #endregion
}