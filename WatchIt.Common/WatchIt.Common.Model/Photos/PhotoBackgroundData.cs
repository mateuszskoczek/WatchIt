using System.Text.Json.Serialization;

namespace WatchIt.Common.Model.Photos;

public class PhotoBackgroundData
{
    #region PROPERTIES
    
    [JsonPropertyName("is_universal_background")]
    public required bool IsUniversalBackground { get; set; }
    
    [JsonPropertyName("first_gradient_color")]
    public required byte[] FirstGradientColor { get; set; }
    
    [JsonPropertyName("second_gradient_color")]
    public required byte[] SecondGradientColor { get; set; }
    
    #endregion
}