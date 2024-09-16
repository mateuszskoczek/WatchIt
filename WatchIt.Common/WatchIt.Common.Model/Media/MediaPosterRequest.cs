using WatchIt.Database.Model.Media;

namespace WatchIt.Common.Model.Media;

public class MediaPosterRequest : MediaPoster
{
    public MediaPosterImage CreateMediaPosterImage() => new MediaPosterImage
    {
        Image = Image,
        MimeType = MimeType,
    };
    
    public void UpdateMediaPosterImage(MediaPosterImage item)
    {
        item.Image = Image;
        item.MimeType = MimeType;
        item.UploadDate = DateTime.Now;
    }
}