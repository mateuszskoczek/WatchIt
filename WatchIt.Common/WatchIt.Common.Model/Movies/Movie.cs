using System.Text.Json.Serialization;

namespace WatchIt.Common.Model.Movies;

public class Movie : Media.Media
{
    [JsonPropertyName("budget")]
    public decimal? Budget { get; set; }
}