using WatchIt.Database.Model.Rating;

namespace WatchIt.Database.Model.Person;

public class PersonCreatorRole
{
    #region PROPERTIES

    public Guid Id { get; set; }
    public required long PersonId { get; set; }
    public required long MediaId { get; set; }
    public required short PersonCreatorRoleTypeId { get; set; }

    #endregion



    #region NAVIGATION

    public virtual Person Person { get; set; } = null!;
    public virtual Media.Media Media { get; set; } = null!;
    public virtual PersonCreatorRoleType PersonCreatorRoleType { get; set; } = null!;
    public virtual IEnumerable<RatingPersonCreatorRole> RatingPersonCreatorRole { get; set; } = new List<RatingPersonCreatorRole>();

    #endregion
}