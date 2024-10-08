using WatchIt.Database.Model.Person;

namespace WatchIt.Common.Model.Roles;

public abstract class CreatorRoleRequest : CreatorRole
{
    #region PUBLIC METHODS
    
    public PersonCreatorRole CreateCreatorRole(long mediaId, long personId) => new PersonCreatorRole
    {
        MediaId = mediaId,
        PersonId = personId,
        PersonCreatorRoleTypeId = TypeId,
    };
    
    public void UpdateCreatorRole(PersonCreatorRole item, long mediaId, long personId)
    {
        item.MediaId = mediaId;
        item.PersonId = personId;
        item.PersonCreatorRoleTypeId = TypeId;
    }

    #endregion
}