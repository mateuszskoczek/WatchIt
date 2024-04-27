namespace WatchIt.Database.Model.Person;

public class PersonPhotoImage
{
    #region PROPERTIES

    public Guid Id { get; set; }
    public required byte[] Image { get; set; }
    public required string MimeType { get; set; }
    public DateTime UploadDate { get; set; }

    #endregion



    #region NAVIGATION

    public virtual Person Person { get; set; } = null!;

    #endregion
}