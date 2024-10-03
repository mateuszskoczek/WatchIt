using System.Diagnostics.CodeAnalysis;
using WatchIt.Database.Model.Media;

namespace WatchIt.Common.Model.Media;

public class MediaPosterRequest : MediaPoster
{
    #region CONSTRUCTORS

    public MediaPosterRequest() {}

    [SetsRequiredMembers]
    public MediaPosterRequest(Picture image)
    {
        Image = image.Image;
        MimeType = image.MimeType;
    }
    
    #endregion
    
    
    public MediaPosterImage CreateMediaPosterImage() => new MediaPosterImage
    {
        Image = Image,
        MimeType = MimeType,
    };
    
    public void UpdateMediaPosterImage(MediaPosterImage item)
    {
        item.Image = Image;
        item.MimeType = MimeType;
        item.UploadDate = DateTime.UtcNow;
    }
}