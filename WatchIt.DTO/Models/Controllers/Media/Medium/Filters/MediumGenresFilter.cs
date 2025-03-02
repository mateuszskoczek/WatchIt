using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.Media.Medium.Filters;

public record MediumGenresFilter<T> : Filter<T> where T : Database.Model.Media.Medium
{
    public MediumGenresFilter(IEnumerable<short>? query) : base(x =>
    (
        query == null
        || 
        query.All(y => x.Genres.Select(z => z.Id).Contains(y))
    )) { }
}