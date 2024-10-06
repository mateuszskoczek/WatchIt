using WatchIt.Database.Model.Person;

namespace WatchIt.Common.Model.Roles;

public class CreatorRoleRequest : CreatorRole, ICreatorRoleMediaRequest, ICreatorRolePersonRequest
{
    #region PUBLIC METHODS

    PersonCreatorRole ICreatorRoleMediaRequest.CreateCreatorRole(long mediaId)
    {
        this.MediaId = mediaId;
        return CreateCreatorRole();
    }
    
    public PersonCreatorRole CreateCreatorRole() => new PersonCreatorRole
    {
        MediaId = MediaId,
        PersonId = PersonId,
        PersonCreatorRoleTypeId = TypeId,
    };

    #endregion
}