using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace WatchIt.Common.Model.Accounts;

public class AccountProfilePictureResponse : AccountProfilePicture
{
    #region PROPERTIES

    [JsonPropertyName("id")]
    public required Guid Id { get; set; }
    
    [JsonPropertyName("upload_date")]
    public required DateTime UploadDate { get; set; }
    
    #endregion
    
    
    
    #region CONSTRUCTORS
    
    [JsonConstructor]
    public AccountProfilePictureResponse() {}

    [SetsRequiredMembers]
    public AccountProfilePictureResponse(Database.Model.Account.AccountProfilePicture accountProfilePicture)
    {
        Id = accountProfilePicture.Id;
        Image = accountProfilePicture.Image;
        MimeType = accountProfilePicture.MimeType;
        UploadDate = accountProfilePicture.UploadDate;
    }
    
    #endregion
}