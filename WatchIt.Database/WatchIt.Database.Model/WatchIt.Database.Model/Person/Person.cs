using WatchIt.Database.Model.Common;
using WatchIt.Database.Model.ViewCount;

namespace WatchIt.Database.Model.Person;

public class Person
{
    #region PROPERTIES

    public long Id { get; set; }
    public required string Name { get; set; }
    public string? FullName { get; set; }
    public string? Description { get; set; }
    public DateOnly? BirthDate { get; set; }
    public DateOnly? DeathDate { get; set; }
    public short? GenderId { get; set; }
    public Guid? PersonPhotoId { get; set; }

    #endregion



    #region NAVIGATION

    public virtual Gender? Gender { get; set; }
    public virtual PersonPhotoImage? PersonPhoto { get; set; }
    public virtual IEnumerable<PersonActorRole> PersonActorRoles { get; set; } = new List<PersonActorRole>();
    public virtual IEnumerable<PersonCreatorRole> PersonCreatorRoles { get; set; } = new List<PersonCreatorRole>();
    public virtual IEnumerable<ViewCountPerson> ViewCountsPerson { get; set; } = new List<ViewCountPerson>();

    #endregion
}