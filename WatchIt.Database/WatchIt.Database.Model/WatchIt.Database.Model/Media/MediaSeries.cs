namespace WatchIt.Database.Model.Media;

public class MediaSeries
{
    #region PROPERTIES

    public required long Id { get; set; }
    public bool HasEnded { get; set; }

    #endregion



    #region NAVIGATION

    public virtual Media Media { get; set; } = null!;
    public virtual IEnumerable<MediaSeriesSeason> MediaSeriesSeasons { get; set; } = new List<MediaSeriesSeason>();

    #endregion
}