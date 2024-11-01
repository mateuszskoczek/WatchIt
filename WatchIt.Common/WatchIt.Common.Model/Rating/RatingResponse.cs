using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using WatchIt.Database.Model.Person;
using WatchIt.Database.Model.Rating;

namespace WatchIt.Common.Model.Rating;

public class RatingResponse
{
    #region PROPERTIES
    
    [JsonPropertyName("average")]
    public required decimal Average { get; set; }
    
    [JsonPropertyName("count")]
    public required long Count { get; set; }
    
    #endregion
    
    
    
    #region CONSTRUCTORS

    [JsonConstructor]
    public RatingResponse() {}

    [SetsRequiredMembers]
    internal RatingResponse(long ratingSum, long ratingCount)
    {
        Average = ratingCount > 0 ? (decimal)ratingSum / ratingCount : 0;
        Count = ratingCount;
    }

    #endregion
}