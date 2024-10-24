using System.Text.Json.Serialization;

namespace WatchIt.Common.Model.Genders;

public class Gender
{
    #region PROPERTIES

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    #endregion
}