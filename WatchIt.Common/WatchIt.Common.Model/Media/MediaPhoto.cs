using System.Text.Json.Serialization;

namespace WatchIt.Common.Model.Media;

public class MediaPhoto
{
    [JsonPropertyName("media_id")]
    public required long MediaId { get; set; }
    
    [JsonPropertyName("image")]
    public required byte[] Image { get; set; }
    
    [JsonPropertyName("mime_type")]
    public required string MimeType { get; set; }

    [JsonPropertyName("background")]
    public MediaPhotoBackground? Background { get; set; }
}