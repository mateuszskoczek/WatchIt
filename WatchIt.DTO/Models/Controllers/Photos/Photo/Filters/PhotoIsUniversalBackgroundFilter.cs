using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.Photos.Photo.Filters;

public record PhotoIsUniversalBackgroundFilter : Filter<Database.Model.Photos.Photo>
{
    public PhotoIsUniversalBackgroundFilter(bool? query) : base(x =>
    (
        query == null
        ||
        (
            x.Background != null
            &&
            x.Background.IsUniversal == query
        )
    )) { }
}