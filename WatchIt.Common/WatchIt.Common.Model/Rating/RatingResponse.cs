using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
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
    public RatingResponse(IEnumerable<RatingMedia> ratingMedia) : this(ratingMedia.Any() ? (decimal)ratingMedia.Average(x => x.Rating) : 0, ratingMedia.Count()) {}

    [SetsRequiredMembers]
    public RatingResponse(decimal ratingAverage, long ratingCount)
    {
        Average = ratingAverage;
        Count = ratingCount;
    }

    #endregion
}