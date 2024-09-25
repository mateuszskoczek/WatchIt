using System.Text.Json.Serialization;

namespace WatchIt.Common.Model;

public abstract class Picture
{
    #region PROPERTIES
    
    [JsonPropertyName("image")]
    public required byte[] Image { get; set; }
    
    [JsonPropertyName("mime_type")]
    public required string MimeType { get; set; }
    
    #endregion
}