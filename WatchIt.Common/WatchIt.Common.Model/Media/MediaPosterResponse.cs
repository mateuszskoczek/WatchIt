using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using WatchIt.Database.Model.Media;

namespace WatchIt.Common.Model.Media;

public class MediaPosterResponse : MediaPoster
{
    #region PROPERTIES
    
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    [JsonPropertyName("upload_date")]
    public DateTime UploadDate { get; set; }
    
    #endregion
    
    
    
    #region CONSTRUCTORS
    
    [JsonConstructor]
    public MediaPosterResponse() {}
    
    [SetsRequiredMembers]
    public MediaPosterResponse(MediaPosterImage mediaPhotoImage)
    {
        Id = mediaPhotoImage.Id;
        Image = mediaPhotoImage.Image;
        MimeType = mediaPhotoImage.MimeType;
        UploadDate = mediaPhotoImage.UploadDate;
    }
    
    #endregion
}