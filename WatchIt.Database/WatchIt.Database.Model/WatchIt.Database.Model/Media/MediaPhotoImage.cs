namespace WatchIt.Database.Model.Media;

public class MediaPhotoImage
{
    #region PROPERTIES

    public Guid Id { get; set; }
    public required long MediaId { get; set; }
    public required byte[] Image { get; set; }
    public required string MimeType { get; set; }
    public DateTime UploadDate { get; set; }

    #endregion



    #region NAVIGATION

    public virtual Media Media { get; set; } = null!;
    public virtual MediaPhotoImageBackground? MediaPhotoImageBackground { get; set; }

    #endregion
}