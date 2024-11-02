using WatchIt.Database.Model.Person;

namespace WatchIt.Database.Model.Rating;

public class RatingPersonActorRole
{
    #region PROPERTIES

    public Guid Id { get; set; }
    public required Guid PersonActorRoleId { get; set; }
    public required long AccountId { get; set; }
    public required short Rating { get; set; }
    public DateTime Date { get; set; }

    #endregion



    #region NAVIGATION

    public virtual PersonActorRole PersonActorRole { get; set; } = null!;
    public virtual Account.Account Account { get; set; } = null!;

    #endregion
}