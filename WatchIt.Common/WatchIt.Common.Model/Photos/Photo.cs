using System.Text.Json.Serialization;

namespace WatchIt.Common.Model.Photos;

public abstract class Photo : Picture
{
    #region PROPERTIES
    
    [JsonPropertyName("background")]
    public PhotoBackgroundData? Background { get; set; }
    
    #endregion
}