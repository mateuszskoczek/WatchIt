using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using WatchIt.Common.Query;
using WatchIt.Database.Model.Media;

namespace WatchIt.Common.Model.Photos;

public class PhotoResponse : Photo, IQueryOrderable<PhotoResponse>
{
    #region PROPERTIES
    
    [JsonIgnore]
    public static IDictionary<string, Func<PhotoResponse, IComparable>> OrderableProperties { get; } = new Dictionary<string, Func<PhotoResponse, IComparable>>
    {
        { "id", x => x.Id },
        { "media_id", x => x.MediaId },
        { "mime_type", x => x.MimeType },
        { "is_background", x => x.Background is not null },
        { "is_universal_background", x => x.Background is not null && x.Background.IsUniversalBackground }
    };

    
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    [JsonPropertyName("media_id")]
    public required long MediaId { get; set; }
    
    [JsonPropertyName("upload_date")]
    public DateTime UploadDate { get; set; }
    
    #endregion
    
    
    
    #region CONSTRUCTORS
    
    [JsonConstructor]
    public PhotoResponse() {}
    
    [SetsRequiredMembers]
    public PhotoResponse(MediaPhotoImage mediaPhotoImage)
    {
        Id = mediaPhotoImage.Id;
        MediaId = mediaPhotoImage.MediaId;
        Image = mediaPhotoImage.Image;
        MimeType = mediaPhotoImage.MimeType;
        UploadDate = mediaPhotoImage.UploadDate;
        
        if (mediaPhotoImage.MediaPhotoImageBackground is not null)
        {
            Background = new PhotoBackgroundData
            {
                IsUniversalBackground = mediaPhotoImage.MediaPhotoImageBackground.IsUniversalBackground,
                FirstGradientColor = mediaPhotoImage.MediaPhotoImageBackground.FirstGradientColor,
                SecondGradientColor = mediaPhotoImage.MediaPhotoImageBackground.SecondGradientColor
            };
        }
    }
    
    #endregion
}