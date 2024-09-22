using System.Diagnostics.CodeAnalysis;
using WatchIt.Database.Model.Media;

namespace WatchIt.Common.Model.Media;

public class MediaPosterRequest : MediaPoster
{
    #region CONSTRUCTORS

    public MediaPosterRequest() {}

    [SetsRequiredMembers]
    public MediaPosterRequest(MediaPosterResponse response)
    {
        Image = response.Image;
        MimeType = response.MimeType;
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