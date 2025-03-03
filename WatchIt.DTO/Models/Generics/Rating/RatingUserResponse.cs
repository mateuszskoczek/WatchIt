using System.Text.Json.Serialization;

namespace WatchIt.DTO.Models.Generics.Rating;

public class RatingUserResponse : IRatingUserResponse
{
    #region PROPERTIES
    
    public byte Rating { get; set; }
    public DateTimeOffset Date { get; set; }
    
    [JsonIgnore] decimal? IRatingResponse.Rating => Rating;
    [JsonIgnore] DateTimeOffset? IRatingUserResponse.Date => Date;
    
    #endregion
}