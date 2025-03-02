namespace WatchIt.DTO.Models.Generics.Rating;

public class RatingOverallResponse : IRatingOverallResponse
{
    #region PROPERTIES
    
    public decimal? Rating { get; set; }
    public long Count { get; set; }
    
    #endregion
}