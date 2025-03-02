using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.Photos.Photo.Filters;

public record PhotoIsBackgroundFilter : Filter<Database.Model.Photos.Photo>
{
    public PhotoIsBackgroundFilter(bool? query) : base(x =>
    (
        query == null
        ||
        (x.Background != null) == query
    )) { }
}