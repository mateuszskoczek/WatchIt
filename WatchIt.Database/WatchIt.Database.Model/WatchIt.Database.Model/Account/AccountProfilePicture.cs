namespace WatchIt.Database.Model.Account;

public class AccountProfilePicture
{
    #region PROPERTIES

    public Guid Id { get; set; }
    public required byte[] Image { get; set; }
    public required string MimeType { get; set; }
    public DateTime UploadDate { get; set; }

    #endregion



    #region NAVIGATION

    public virtual Account Account { get; set; } = null!;

    #endregion
}