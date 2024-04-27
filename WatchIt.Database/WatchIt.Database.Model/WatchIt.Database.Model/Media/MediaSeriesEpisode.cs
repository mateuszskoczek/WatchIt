using WatchIt.Database.Model.Rating;

namespace WatchIt.Database.Model.Media;

public class MediaSeriesEpisode
{
    #region PROPERTIES

    public Guid Id { get; set; }
    public required Guid MediaSeriesSeasonId { get; set; }
    public required short Number { get; set; }
    public string? Name { get; set; }
    public bool IsSpecial { get; set; }

    #endregion



    #region NAVIGATION

    public virtual MediaSeriesSeason MediaSeriesSeason { get; set; } = null!;
    public virtual IEnumerable<RatingMediaSeriesEpisode> RatingMediaSeriesEpisode { get; set; } = new List<RatingMediaSeriesEpisode>();

    #endregion
}