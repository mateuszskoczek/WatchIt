using WatchIt.Database.Model.Media;

namespace WatchIt.Common.Model.Media;

public class MediaPhotoRequest : MediaPhoto
{
    public MediaPhotoImage CreateMediaPhotoImage() => new MediaPhotoImage
    {
        MediaId = MediaId,
        Image = Image,
        MimeType = MimeType
    };

    public MediaPhotoImageBackground? CreateMediaPhotoImageBackground(Guid mediaPhotoImageId) => Background is null ? null : new MediaPhotoImageBackground
    {
        Id = mediaPhotoImageId,
        IsUniversalBackground = Background.IsUniversalBackground,
        FirstGradientColor = Background.FirstGradientColor,
        SecondGradientColor = Background.SecondGradientColor
    };
    
    public void UpdateMediaPhotoImage(MediaPhotoImage item)
    {
        item.MediaId = MediaId;
        item.Image = Image;
        item.MimeType = MimeType;
    }

    public void UpdateMediaPhotoImageBackground(MediaPhotoImageBackground item)
    {
        if (Background is not null)
        {
            item.IsUniversalBackground = Background.IsUniversalBackground;
            item.FirstGradientColor = Background.FirstGradientColor;
            item.SecondGradientColor = Background.SecondGradientColor;
        }
    }
}