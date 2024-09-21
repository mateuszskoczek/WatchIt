using System.Text.Json.Serialization;

namespace WatchIt.Common.Model.Series;

public class Series : Media.Media
{
    [JsonPropertyName("has_ended")]
    public bool HasEnded { get; set; }
}