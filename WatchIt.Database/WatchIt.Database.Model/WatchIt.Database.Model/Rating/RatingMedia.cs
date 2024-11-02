namespace WatchIt.Database.Model.Rating;

public class RatingMedia
{
    #region PROPERTIES

    public Guid Id { get; set; }
    public required long MediaId { get; set; }
    public required long AccountId { get; set; }
    public required short Rating { get; set; }
    public DateTime Date { get; set; }

    #endregion



    #region NAVIGATION

    public virtual Model.Media.Media Media { get; set; } = null!;
    public virtual Model.Account.Account Account { get; set; } = null!;

    #endregion
}