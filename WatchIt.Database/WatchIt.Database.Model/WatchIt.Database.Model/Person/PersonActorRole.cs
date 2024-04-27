using WatchIt.Database.Model.Rating;

namespace WatchIt.Database.Model.Person;

public class PersonActorRole
{
    #region PROPERTIES

    public Guid Id { get; set; }
    public required long PersonId { get; set; }
    public required long MediaId { get; set; }
    public required short PersonActorRoleTypeId { get; set; }
    public required string RoleName { get; set; }

    #endregion



    #region NAVIGATION

    public virtual Person Person { get; set; } = null!;
    public virtual Media.Media Media { get; set; } = null!;
    public virtual PersonActorRoleType PersonActorRoleType { get; set; } = null!;

    public virtual IEnumerable<RatingPersonActorRole> RatingPersonActorRole { get; set; } = new List<RatingPersonActorRole>();

    #endregion
}