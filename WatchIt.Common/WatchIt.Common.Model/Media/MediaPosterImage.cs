using System.Text.Json.Serialization;

namespace WatchIt.Common.Model.Media;

public class MediaPosterImage
{
    [JsonPropertyName("image")]
    public required byte[] Image { get; set; }
    
    [JsonPropertyName("mime_type")]
    public required string MimeType { get; set; }
}