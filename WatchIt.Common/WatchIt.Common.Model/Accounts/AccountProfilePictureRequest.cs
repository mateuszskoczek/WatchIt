using System.Diagnostics.CodeAnalysis;

namespace WatchIt.Common.Model.Accounts;

public class AccountProfilePictureRequest : AccountProfilePicture
{
    #region CONSTRUCTORS

    public AccountProfilePictureRequest() {}

    [SetsRequiredMembers]
    public AccountProfilePictureRequest(Picture image)
    {
        Image = image.Image;
        MimeType = image.MimeType;
    }
    
    #endregion
    
    
    public Database.Model.Account.AccountProfilePicture CreateMediaPosterImage() => new Database.Model.Account.AccountProfilePicture
    {
        Image = Image,
        MimeType = MimeType,
    };
    
    public void UpdateMediaPosterImage(Database.Model.Account.AccountProfilePicture item)
    {
        item.Image = Image;
        item.MimeType = MimeType;
        item.UploadDate = DateTime.UtcNow;
    }
}