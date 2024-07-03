using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using WatchIt.Database.Model.Media;

namespace WatchIt.Common.Model.Media;

public class MediaPhotoResponse : MediaPhoto
{
    #region PROPERTIES
    
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    [JsonPropertyName("upload_date")]
    public DateTime UploadDate { get; set; }
    
    #endregion
    
    
    
    #region CONSTRUCTORS
    
    [JsonConstructor]
    public MediaPhotoResponse() {}
    
    [SetsRequiredMembers]
    public MediaPhotoResponse(MediaPhotoImage mediaPhotoImage)
    {
        Id = mediaPhotoImage.Id;
        MediaId = mediaPhotoImage.MediaId;
        Image = mediaPhotoImage.Image;
        MimeType = mediaPhotoImage.MimeType;
        UploadDate = mediaPhotoImage.UploadDate;
        
        if (mediaPhotoImage.MediaPhotoImageBackground is not null)
        {
            Background = new MediaPhotoBackground
            {
                IsUniversalBackground = mediaPhotoImage.MediaPhotoImageBackground.IsUniversalBackground,
                FirstGradientColor = mediaPhotoImage.MediaPhotoImageBackground.FirstGradientColor,
                SecondGradientColor = mediaPhotoImage.MediaPhotoImageBackground.SecondGradientColor
            };
        }
    }
    
    #endregion
}