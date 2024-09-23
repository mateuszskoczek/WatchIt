using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace WatchIt.Common.Model.Media;

public class MediaRatingResponse
{
    #region PROPERTIES
    
    [JsonPropertyName("rating_average")]
    public required double RatingAverage { get; set; }
    
    [JsonPropertyName("rating_count")]
    public required long RatingCount { get; set; }
    
    #endregion
    
    
    
    #region CONSTRUCTORS

    [JsonConstructor]
    public MediaRatingResponse() {}
    
    [SetsRequiredMembers]
    public MediaRatingResponse(double ratingAverage, long ratingCount)
    {
        RatingAverage = ratingAverage;
        RatingCount = ratingCount;
    }

    #endregion
}