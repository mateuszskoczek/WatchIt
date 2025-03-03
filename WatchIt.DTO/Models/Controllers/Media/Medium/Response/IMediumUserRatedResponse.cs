using WatchIt.DTO.Models.Generics.Rating;

namespace WatchIt.DTO.Models.Controllers.Media.Medium.Response;

public interface IMediumUserRatedResponse
{
    #region PROPERTIES
    
    RatingUserResponse? RatingUser { get; set; }
    
    #endregion
}