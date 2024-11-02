using WatchIt.Database.Model.Media;

namespace WatchIt.Database.Model.Rating;

public class RatingMediaSeriesEpisode
{
    #region PROPERTIES

    public Guid Id { get; set; }
    public required Guid MediaSeriesEpisodeId { get; set; }
    public required long AccountId { get; set; }
    public required short Rating { get; set; }
    public DateTime Date { get; set; }

    #endregion



    #region NAVIGATION

    public virtual MediaSeriesEpisode MediaSeriesEpisode { get; set; } = null!;
    public virtual Account.Account Account { get; set; } = null!;

    #endregion
}