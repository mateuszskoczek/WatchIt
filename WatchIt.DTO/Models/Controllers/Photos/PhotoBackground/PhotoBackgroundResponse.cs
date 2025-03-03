using System.Drawing;

namespace WatchIt.DTO.Models.Controllers.Photos.PhotoBackground;

public class PhotoBackgroundResponse
{
    #region PROPERTIES

    public Guid Id { get; set; }
    public bool IsUniversal { get; set; }
    public Color FirstGradientColor { get; set; }
    public Color SecondGradientColor { get; set; }

    #endregion
}