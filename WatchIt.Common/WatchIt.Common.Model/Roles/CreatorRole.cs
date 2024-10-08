using System.Text.Json.Serialization;

namespace WatchIt.Common.Model.Roles;

public abstract class CreatorRole
{
    #region PROPERTIES
    
    [JsonPropertyName("type_id")]
    public required short TypeId { get; set; }
    
    #endregion
}