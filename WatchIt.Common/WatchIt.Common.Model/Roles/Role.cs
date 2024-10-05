using System.Text.Json.Serialization;

namespace WatchIt.Common.Model.Roles;

public class Role
{
    #region PROPERTIES

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    #endregion
}