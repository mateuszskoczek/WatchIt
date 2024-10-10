using System.Text.Json.Serialization;
using WatchIt.Database.Model.Person;

namespace WatchIt.Common.Model.Roles;

public class ActorRoleMediaRequest : ActorRoleRequest, IActorRoleMediaRequest
{
    #region PROPERTIES
    
    [JsonPropertyName("person_id")]
    public long PersonId { get; set; }
    
    #endregion



    #region PUBLIC METHODS

    public PersonActorRole CreateActorRole(long mediaId) => base.CreateActorRole(mediaId, PersonId);

    #endregion
}