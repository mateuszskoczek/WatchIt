using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace WatchIt.Common.Model.Rating;

public class RatingRequest
{
    #region PROPERTIES
    
    [JsonPropertyName("rating")]
    public required short Rating { get; set; }

    #endregion
    
    
    
    #region CONSTRUCTORS

    [SetsRequiredMembers]
    public RatingRequest(short rating)
    {
        Rating = rating;
    }
    
    #endregion
}