using WatchIt.Database.Model.Accounts;

namespace WatchIt.Database.Model.Genders;

public class Gender
{
    #region PROPERTIES

    public short Id { get; set; }
    public string Name { get; set; } = default!;
    public uint Version { get; set; }

    #endregion
    
    
    
    #region NAVIGATION
    
    // Accounts
    public virtual IEnumerable<Account> Accounts { get; set; } = new List<Account>();
    
    // People
    public virtual IEnumerable<People.Person> People { get; set; } = new List<People.Person>();
    
    #endregion
}