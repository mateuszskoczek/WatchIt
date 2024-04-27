using WatchIt.Database.Model.Rating;

namespace WatchIt.Database.Model.Media;

public class MediaSeriesSeason
{
    #region PROPERTIES

    public Guid Id { get; set; }
    public required long MediaSeriesId { get; set; }
    public required short Number { get; set; }
    public string? Name { get; set; }

    #endregion



    #region NAVIGATION

    public virtual MediaSeries MediaSeries { get; set; } = null!;
    public virtual IEnumerable<MediaSeriesEpisode> MediaSeriesEpisodes { get; set; } = new List<MediaSeriesEpisode>();
    public virtual IEnumerable<RatingMediaSeriesSeason> RatingMediaSeriesSeason { get; set; } = new List<RatingMediaSeriesSeason>();

    #endregion
}