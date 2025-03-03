namespace WatchIt.Database.Model.People;

public class PersonViewCount : IViewCountEntity
{
    #region PROPERTIES
    
    public long PersonId { get; set; }
    public DateOnly Date { get; set; }
    public long ViewCount { get; set; }
    public uint Version { get; set; }
    
    #endregion



    #region NAVIGATION

    public virtual Person Person { get; set; } = default!;

    #endregion
}