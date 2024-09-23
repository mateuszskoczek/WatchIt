using System.Text.Json.Serialization;

namespace WatchIt.Common.Model.Media;

public abstract class Media
{
    [JsonPropertyName("title")]
    public required string Title { get; set; }

    [JsonPropertyName("original_title")]
    public string? OriginalTitle { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("release_date")]
    public DateOnly? ReleaseDate { get; set; }

    [JsonPropertyName("length")]
    public short? Length { get; set; }
}