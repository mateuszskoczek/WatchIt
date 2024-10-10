using System.Text.Json.Serialization;
using WatchIt.Database.Model.Person;

namespace WatchIt.Common.Model.Roles;

public class ActorRoleUniversalRequest : ActorRoleRequest, IActorRolePersonRequest, IActorRoleMediaRequest
{
    #region PROPERTIES
    
    [JsonPropertyName("person_id")]
    public long PersonId { get; set; }
    
    [JsonPropertyName("media_id")]
    public long MediaId { get; set; }
    
    #endregion
    
    
    
    #region CONSTRUCTORS
    
    public ActorRoleUniversalRequest() { }

    public ActorRoleUniversalRequest(ActorRoleResponse data)
    {
        MediaId = data.MediaId;
        PersonId = data.PersonId;
        TypeId = data.TypeId;
        Name = data.Name;
    }
    
    #endregion



    #region PUBLIC METHODS

    public PersonActorRole CreateActorRole() => base.CreateActorRole(MediaId, PersonId);
    
    public void UpdateActorRole(PersonActorRole item) => base.UpdateActorRole(item, MediaId, PersonId);

    #endregion
}