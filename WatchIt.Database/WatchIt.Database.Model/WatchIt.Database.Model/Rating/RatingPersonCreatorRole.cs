using WatchIt.Database.Model.Person;

namespace WatchIt.Database.Model.Rating;

public class RatingPersonCreatorRole
{
    #region PROPERTIES

    public Guid Id { get; set; }
    public required Guid PersonCreatorRoleId { get; set; }
    public required long AccountId { get; set; }
    public required short Rating { get; set; }
    public DateTime Date { get; set; }

    #endregion



    #region NAVIGATION

    public virtual PersonCreatorRole PersonCreatorRole { get; set; } = null!;
    public virtual Account.Account Account { get; set; } = null!;

    #endregion
}