using WatchIt.DTO.Models.Controllers.Photos.PhotoBackground;
using WatchIt.DTO.Models.Generics.Image;

namespace WatchIt.DTO.Models.Controllers.Photos.Photo;

public class PhotoRequest : ImageRequest
{
    #region PROPERTIES
    
    public long MediumId { get; set; }
    public PhotoBackgroundRequest? BackgroundData { get; set; }
    
    #endregion
}