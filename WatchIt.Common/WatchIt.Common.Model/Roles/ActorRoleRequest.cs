using WatchIt.Database.Model.Person;

namespace WatchIt.Common.Model.Roles;

public class ActorRoleRequest : ActorRole, IActorRoleMediaRequest, IActorRolePersonRequest
{
    #region PUBLIC METHODS

    PersonActorRole IActorRoleMediaRequest.CreateActorRole(long mediaId)
    {
        MediaId = mediaId;
        return CreateActorRole();
    }
    
    PersonActorRole IActorRolePersonRequest.CreateActorRole(long personId)
    {
        PersonId = personId;
        return CreateActorRole();
    }
    
    public PersonActorRole CreateActorRole() => new PersonActorRole
    {
        MediaId = MediaId,
        PersonId = PersonId,
        PersonActorRoleTypeId = TypeId,
        RoleName = Name,
    };
    
    public void UpdateActorRole(PersonActorRole item)
    {
        item.MediaId = MediaId;
        item.PersonId = PersonId;
        item.PersonActorRoleTypeId = TypeId;
        item.RoleName = Name;
    }

    #endregion
}