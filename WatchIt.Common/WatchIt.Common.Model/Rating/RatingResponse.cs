using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace WatchIt.Common.Model.Rating;

public class RatingResponse
{
    #region PROPERTIES
    
    [JsonPropertyName("rating_average")]
    public required double RatingAverage { get; set; }
    
    [JsonPropertyName("rating_count")]
    public required long RatingCount { get; set; }
    
    #endregion
    
    
    
    #region CONSTRUCTORS

    [JsonConstructor]
    public RatingResponse() {}
    
    [SetsRequiredMembers]
    public RatingResponse(double ratingAverage, long ratingCount)
    {
        RatingAverage = ratingAverage;
        RatingCount = ratingCount;
    }

    #endregion
}