using System.Text.Json.Serialization;

namespace WatchIt.DTO.Models.Generics.Rating;

public class RatingUserOverallResponse : IRatingUserResponse, IRatingOverallResponse
{
    #region PROPERTIES
    
    public decimal? Rating { get; set; }
    public long Count { get; set; }
    public DateTimeOffset? Date { get; set; }
    
    #endregion

}