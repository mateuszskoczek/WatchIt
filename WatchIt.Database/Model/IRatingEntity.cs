using WatchIt.Database.Model.Media;

namespace WatchIt.Database.Model;

public interface IRatingEntity
{
    #region PROPERTIES
    
    long AccountId { get; set; }
    byte Rating { get; set; }
    DateTime Date { get; set; }

    #endregion
    
    
    
    #region NAVIGATION
    
    // Account
    Accounts.Account Account { get; set; }
    
    #endregion
}