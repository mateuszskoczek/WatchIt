namespace WatchIt.Database.Model.Media;

public class MediumRating : IRatingEntity
{
    #region PROPERTIES
    
    public long AccountId { get; set; }
    public long MediumId { get; set; }
    public byte Rating { get; set; }
    public DateTime Date { get; set; }
    public uint Version { get; set; }
    
    #endregion
    
    
    
    #region NAVIGATION
    
    // Account
    public virtual Accounts.Account Account { get; set; } = default!;
    
    // Medium
    public virtual Medium Medium { get; set; } = default!;
    
    #endregion
}