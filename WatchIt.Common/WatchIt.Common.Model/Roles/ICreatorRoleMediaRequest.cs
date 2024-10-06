using System.Text.Json.Serialization;
using WatchIt.Database.Model.Person;

namespace WatchIt.Common.Model.Roles;

public interface ICreatorRoleMediaRequest
{
    #region PROPERTIES
    
    [JsonPropertyName("person_id")]
    long PersonId { get; set; }
    
    [JsonPropertyName("type_id")]
    short TypeId { get; set; }
    
    #endregion
    
    
    
    #region PUBLIC METHODS

    PersonCreatorRole CreateCreatorRole(long mediaId);

    #endregion
}