using System.Text.Json.Serialization;

namespace WatchIt.Common.Model.Media;

public class MediaPhotoBackground
{
    [JsonPropertyName("is_universal_background")]
    public required bool IsUniversalBackground { get; set; }
    
    [JsonPropertyName("first_gradient_color")]
    public required byte[] FirstGradientColor { get; set; }
    
    [JsonPropertyName("second_gradient_color")]
    public required byte[] SecondGradientColor { get; set; }
}