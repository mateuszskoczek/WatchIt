using System.Text.Json.Serialization;
using WatchIt.Database.Model.Person;

namespace WatchIt.Common.Model.Roles;

public interface IActorRoleMediaRequest
{
    #region PROPERTIES
    
    [JsonPropertyName("person_id")]
    long PersonId { get; set; }
    
    [JsonPropertyName("type_id")]
    short TypeId { get; set; }
    
    [JsonPropertyName("name")]
    string Name { get; set; }
    
    #endregion
    
    
    
    #region PUBLIC METHODS

    PersonActorRole CreateActorRole(long mediaId);

    #endregion
}