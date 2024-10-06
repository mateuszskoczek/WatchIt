using System.Text.Json.Serialization;

namespace WatchIt.Common.Model.Roles;

public interface ICreatorRolePersonRequest
{
    #region PROPERTIES
    
    [JsonPropertyName("media_id")]
    long MediaId { get; set; }
    
    [JsonPropertyName("type_id")]
    short TypeId { get; set; }
    
    #endregion
}