using WatchIt.Common.Model.Photos;
using WatchIt.Database.Model.Media;

namespace WatchIt.Common.Model.Media;

public class MediaPhotoRequest : Photo
{
    public MediaPhotoImage CreateMediaPhotoImage(long mediaId) => new MediaPhotoImage
    {
        MediaId = mediaId,
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
}