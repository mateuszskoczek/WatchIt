using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.Photos.Photo.Filters;

public record PhotoMediumIdFilter : Filter<Database.Model.Photos.Photo>
{
    public PhotoMediumIdFilter(long? query) : base(x =>
    (
        query == null
        ||
        x.MediumId == query
    )) { }
}