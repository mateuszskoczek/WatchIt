namespace WatchIt.Database.Model.Account;

public class AccountFollow
{
    #region PROPERTIES
    
    public Guid Id { get; set; }
    public long AccountFollowerId { get; set; }
    public long AccountFollowedId { get; set; }
    
    #endregion
    
    
    
    #region NAVIGATION
    
    public virtual Account AccountFollower { get; set; } = null!;
    public virtual Account AccountFollowed { get; set; } = null!;
    
    #endregion
}