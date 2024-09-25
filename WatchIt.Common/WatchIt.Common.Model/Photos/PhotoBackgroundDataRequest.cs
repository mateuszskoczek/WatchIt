using WatchIt.Database.Model.Media;

namespace WatchIt.Common.Model.Photos;

public class PhotoBackgroundDataRequest : PhotoBackgroundData
{
    #region PUBLIC METHODS

    public MediaPhotoImageBackground CreateMediaPhotoImageBackground(Guid photoId) => new MediaPhotoImageBackground
    {
        Id = photoId,
        IsUniversalBackground = IsUniversalBackground,
        FirstGradientColor = FirstGradientColor,
        SecondGradientColor = SecondGradientColor,
    };

    public void UpdateMediaPhotoImageBackground(MediaPhotoImageBackground image)
    {
        image.IsUniversalBackground = IsUniversalBackground;
        image.FirstGradientColor = FirstGradientColor;
        image.SecondGradientColor = SecondGradientColor;
    }
    
    #endregion
}