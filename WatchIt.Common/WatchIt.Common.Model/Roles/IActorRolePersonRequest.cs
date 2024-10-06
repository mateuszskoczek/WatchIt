using System.Text.Json.Serialization;

namespace WatchIt.Common.Model.Roles;

public interface IActorRolePersonRequest
{
    #region PROPERTIES
    
    [JsonPropertyName("media_id")]
    long MediaId { get; set; }
    
    [JsonPropertyName("type_id")]
    short TypeId { get; set; }
    
    [JsonPropertyName("name")]
    string Name { get; set; }
    
    #endregion
}