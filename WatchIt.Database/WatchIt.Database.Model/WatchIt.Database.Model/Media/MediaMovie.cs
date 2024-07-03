namespace WatchIt.Database.Model.Media;

public class MediaMovie
{
    #region PROPERTIES

    public long Id { get; set; }
    public decimal? Budget { get; set; }

    #endregion



    #region NAVIGATION

    public virtual Media Media { get; set; } = null!;

    #endregion
}