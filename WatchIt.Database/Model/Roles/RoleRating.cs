namespace WatchIt.Database.Model.Roles;

public class RoleRating : IRatingEntity
{
    #region PROPERTIES

    public long AccountId { get; set; }
    public Guid RoleId { get; set; }
    public byte Rating { get; set; }
    public DateTime Date { get; set; }
    public uint Version { get; set; }

    #endregion
    
    
    
    #region NAVIGATION
    
    // Account
    public virtual Accounts.Account Account { get; set; } = default!;
    
    // Role
    public virtual Role Role { get; set; } = default!;
    
    #endregion
}