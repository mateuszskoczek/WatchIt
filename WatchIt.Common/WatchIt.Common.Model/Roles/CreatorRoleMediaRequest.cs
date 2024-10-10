using System.Text.Json.Serialization;
using WatchIt.Database.Model.Person;

namespace WatchIt.Common.Model.Roles;

public class CreatorRoleMediaRequest : CreatorRoleRequest, ICreatorRoleMediaRequest
{
    #region PROPERTIES
    
    [JsonPropertyName("person_id")]
    public long PersonId { get; set; }
    
    #endregion
    
    
    
    #region PUBLIC METHODS
    
    public PersonCreatorRole CreateCreatorRole(long mediaId) => base.CreateCreatorRole(mediaId, PersonId);
    
    #endregion
}