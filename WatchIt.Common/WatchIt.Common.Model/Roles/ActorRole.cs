using System.Text.Json.Serialization;

namespace WatchIt.Common.Model.Roles;

public class ActorRole
{
    #region PROPERTIES
    
    [JsonPropertyName("type_id")]
    public short TypeId { get; set; }
    
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("media_id")]
    public long MediaId { get; set; }
    
    [JsonPropertyName("person_id")]
    public long PersonId { get; set; }
    
    #endregion
}