using WatchIt.Database.Model.Person;

namespace WatchIt.Common.Model.Roles;

public class CreatorRoleRequest : CreatorRole, ICreatorRoleMediaRequest, ICreatorRolePersonRequest
{
    #region PUBLIC METHODS

    PersonCreatorRole ICreatorRoleMediaRequest.CreateCreatorRole(long mediaId)
    {
        MediaId = mediaId;
        return CreateCreatorRole();
    }
    
    PersonCreatorRole ICreatorRolePersonRequest.CreateCreatorRole(long personId)
    {
        PersonId = personId;
        return CreateCreatorRole();
    }
    
    public PersonCreatorRole CreateCreatorRole() => new PersonCreatorRole
    {
        MediaId = MediaId,
        PersonId = PersonId,
        PersonCreatorRoleTypeId = TypeId,
    };
    
    public void UpdateCreatorRole(PersonCreatorRole item)
    {
        item.MediaId = MediaId;
        item.PersonId = PersonId;
        item.PersonCreatorRoleTypeId = TypeId;
    }

    #endregion
}