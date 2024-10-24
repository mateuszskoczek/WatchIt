using System.Text.Json.Serialization;

namespace WatchIt.Common.Model.Roles;

public abstract class ActorRole
{
    #region PROPERTIES
    
    [JsonPropertyName("type_id")]
    public short TypeId { get; set; }
    
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    #endregion
}