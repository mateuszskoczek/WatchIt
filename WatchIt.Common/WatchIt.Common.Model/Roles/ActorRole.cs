using System.Text.Json.Serialization;

namespace WatchIt.Common.Model.Roles;

public class ActorRole
{
    #region PROPERTIES
    
    [JsonPropertyName("type_id")]
    public required short TypeId { get; set; }
    
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    
    [JsonPropertyName("media_id")]
    public required long MediaId { get; set; }
    
    [JsonPropertyName("person_id")]
    public required long PersonId { get; set; }
    
    #endregion
}