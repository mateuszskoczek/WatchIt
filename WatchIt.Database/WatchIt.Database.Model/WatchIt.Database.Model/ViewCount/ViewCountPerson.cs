namespace WatchIt.Database.Model.ViewCount;

public class ViewCountPerson
{
    #region PROPERTIES

    public Guid Id { get; set; }
    public required long PersonId { get; set; }
    public DateOnly Date { get; set; }
    public long ViewCount { get; set; }

    #endregion



    #region NAVIGATION

    public virtual Person.Person Person { get; set; } = null!;

    #endregion
}