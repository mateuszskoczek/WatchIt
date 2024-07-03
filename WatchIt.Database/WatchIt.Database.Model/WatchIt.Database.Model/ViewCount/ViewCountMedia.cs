namespace WatchIt.Database.Model.ViewCount;

public class ViewCountMedia
{
    #region PROPERTIES

    public Guid Id { get; set; }
    public required long MediaId { get; set; }
    public DateOnly Date { get; set; }
    public long ViewCount { get; set; }

    #endregion



    #region NAVIGATION

    public virtual Media.Media Media { get; set; } = null!;

    #endregion
}