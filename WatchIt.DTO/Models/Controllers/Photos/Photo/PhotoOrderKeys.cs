using System.Linq.Expressions;

namespace WatchIt.DTO.Models.Controllers.Photos.Photo;

public static class PhotoOrderKeys
{
    public static readonly Dictionary<string, Expression<Func<Database.Model.Photos.Photo, object?>>> Base = new Dictionary<string, Expression<Func<Database.Model.Photos.Photo, object?>>>
    {
        { "id", x => x.Id },
        { "medium_id", x => x.MediumId },
        { "mime_type", x => x.MimeType },
        { "is_background", x => x.Background != null },
        { "is_universal_background", x => x.Background != null && x.Background.IsUniversal }
    };
}