using System.Text.Json.Serialization;

namespace WatchIt.Common.Model.Accounts;

public abstract class AccountProfilePicture
{
    #region PROPERTIES
    
    [JsonPropertyName("image")]
    public required byte[] Image { get; set; }
    
    [JsonPropertyName("mime_type")]
    public required string MimeType { get; set; }
    
    #endregion



    #region CONSTRUCTORS

    [JsonConstructor]
    public AccountProfilePicture() {}

    #endregion
}