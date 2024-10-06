using WatchIt.Database.Model.Person;

namespace WatchIt.Common.Model.Roles;

public class ActorRoleRequest : ActorRole, IActorRoleMediaRequest, IActorRolePersonRequest
{
    #region PUBLIC METHODS

    PersonActorRole IActorRoleMediaRequest.CreateActorRole(long mediaId)
    {
        this.MediaId = mediaId;
        return CreateActorRole();
    }
    
    public PersonActorRole CreateActorRole() => new PersonActorRole
    {
        MediaId = MediaId,
        PersonId = PersonId,
        PersonActorRoleTypeId = TypeId,
        RoleName = Name,
    };

    #endregion
}