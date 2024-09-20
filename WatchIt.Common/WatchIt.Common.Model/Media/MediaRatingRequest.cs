using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace WatchIt.Common.Model.Media;

public class MediaRatingRequest
{
    #region PROPERTIES
    
    [JsonPropertyName("rating")]
    public required short Rating { get; set; }

    #endregion
    
    
    
    #region CONSTRUCTORS

    [SetsRequiredMembers]
    public MediaRatingRequest(short rating)
    {
        Rating = rating;
    }
    
    #endregion
}