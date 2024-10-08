using System.Text.Json.Serialization;
using WatchIt.Database.Model.Person;

namespace WatchIt.Common.Model.Roles;

public class ActorRolePersonRequest : ActorRoleRequest, IActorRolePersonRequest
{
    #region PROPERTIES
    
    [JsonPropertyName("media_id")]
    public long MediaId { get; set; }
    
    #endregion



    #region PUBLIC METHODS

    public PersonActorRole CreateActorRole(long personId) => base.CreateActorRole(MediaId, personId);

    #endregion
}