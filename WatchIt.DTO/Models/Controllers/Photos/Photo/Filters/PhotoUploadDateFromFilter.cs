using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.Photos.Photo.Filters;

public record PhotoUploadDateFromFilter : Filter<Database.Model.Photos.Photo>
{
    public PhotoUploadDateFromFilter(DateOnly? query) : base(x =>
    (
        query == null
        ||
        x.UploadDate.DateTime.CompareTo(query.Value) >= 0
    )) { }
}