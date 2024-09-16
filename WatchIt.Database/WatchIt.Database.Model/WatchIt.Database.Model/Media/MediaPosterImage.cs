namespace WatchIt.Database.Model.Media;

public class MediaPosterImage
{
    #region PROPERTIES

    public Guid Id { get; set; }
    public required byte[] Image { get; set; }
    public required string MimeType { get; set; }
    public DateTime UploadDate { get; set; }

    #endregion



    #region NAVIGATION

    public virtual Media Media { get; set; } = null!;

    #endregion
}