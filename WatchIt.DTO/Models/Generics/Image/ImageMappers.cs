using WatchIt.Database.Model;

namespace WatchIt.DTO.Models.Generics.Image;

public static class ImageMappers
{
    #region PUBLIC METHODS
    
    public static ImageResponse ToResponse(this IImageEntity entity) => new ImageResponse
    {
        Image = entity.Image,
        MimeType = entity.MimeType,
        UploadDate = entity.UploadDate,
    };
    
    public static void UpdateWithRequest(this IImageEntity entity, ImageRequest request)
    {
        entity.Image = request.Image;
        entity.MimeType = request.MimeType;
        entity.UploadDate = DateTimeOffset.UtcNow;
    }
    
    #endregion
}