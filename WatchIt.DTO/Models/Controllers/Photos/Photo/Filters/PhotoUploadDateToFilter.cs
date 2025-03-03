using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.Photos.Photo.Filters;

public record PhotoUploadDateToFilter : Filter<Database.Model.Photos.Photo>
{
    public PhotoUploadDateToFilter(DateOnly? query) : base(x =>
    (
        query == null
        ||
        x.UploadDate.DateTime.CompareTo(query.Value) <= 0
    )) { }
}