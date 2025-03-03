using WatchIt.DTO.Models.Controllers.Photos.PhotoBackground;
using WatchIt.DTO.Models.Generics.Image;

namespace WatchIt.DTO.Models.Controllers.Photos.Photo;

public class PhotoResponse : ImageResponse
{
    #region PROPERTIES
    
    public Guid Id { get; set; }
    public long MediumId { get; set; }
    public PhotoBackgroundResponse? Background { get; set; }
    
    #endregion
}