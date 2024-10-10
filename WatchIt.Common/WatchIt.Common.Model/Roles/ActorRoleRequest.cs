using System.Diagnostics.CodeAnalysis;
using WatchIt.Database.Model.Person;

namespace WatchIt.Common.Model.Roles;

public abstract class ActorRoleRequest : ActorRole, IActorRoleRequest
{
    #region PUBLIC METHODS
    
    public PersonActorRole CreateActorRole(long mediaId, long personId) => new PersonActorRole
    {
        MediaId = mediaId,
        PersonId = personId,
        PersonActorRoleTypeId = TypeId,
        RoleName = Name,
    };
    
    public void UpdateActorRole(PersonActorRole item, long mediaId, long personId)
    {
        item.MediaId = mediaId;
        item.PersonId = personId;
        item.PersonActorRoleTypeId = TypeId;
        item.RoleName = Name;
    }

    #endregion
}