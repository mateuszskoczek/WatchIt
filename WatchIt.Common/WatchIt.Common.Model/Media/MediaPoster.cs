using System.Text.Json.Serialization;

namespace WatchIt.Common.Model.Media;

public abstract class MediaPoster : Picture
{
    #region PUBLIC METHODS

    public override string ToString() => $"data:{MimeType};base64,{Convert.ToBase64String(Image)}";

    #endregion
}