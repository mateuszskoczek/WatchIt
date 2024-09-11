using System.Text.Json.Serialization;

namespace WatchIt.Common.Model.Media;

public abstract class MediaPoster
{
    [JsonPropertyName("image")]
    public required byte[] Image { get; set; }
    
    [JsonPropertyName("mime_type")]
    public required string MimeType { get; set; }
}