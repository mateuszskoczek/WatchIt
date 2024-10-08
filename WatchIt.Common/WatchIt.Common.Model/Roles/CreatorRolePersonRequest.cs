using System.Text.Json.Serialization;
using WatchIt.Database.Model.Person;

namespace WatchIt.Common.Model.Roles;

public class CreatorRolePersonRequest : CreatorRoleRequest
{
    #region PROPERTIES
    
    [JsonPropertyName("media_id")]
    public required long MediaId { get; set; }
    
    #endregion
    
    
    
    #region PUBLIC METHODS
    
    public PersonCreatorRole CreateCreatorRole(long personId) => base.CreateCreatorRole(MediaId, personId);
    
    #endregion
}