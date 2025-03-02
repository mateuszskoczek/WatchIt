using WatchIt.DTO.Models.Generics.Rating;

namespace WatchIt.DTO.Models.Controllers.Media.Medium.Response;

public class MediumMovieUserRatedResponse : MediumMovieResponse, IMediumUserRatedResponse
{
    #region PROPERTIES
    
    public RatingUserResponse? RatingUser { get; set; }
    
    #endregion
}