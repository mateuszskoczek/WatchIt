using System.Text.Json.Serialization;

namespace WatchIt.Common.Model.Genres;

public class Genre
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    
    [JsonPropertyName("description")]
    public string? Description { get; set; }
}