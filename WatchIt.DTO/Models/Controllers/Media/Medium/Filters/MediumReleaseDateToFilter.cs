using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.Media.Medium.Filters;

public record MediumReleaseDateToFilter<T> : Filter<T> where T : Database.Model.Media.Medium
{
    public MediumReleaseDateToFilter(DateOnly? query) : base(x =>
    (
        query == null
        ||
        (
            x.ReleaseDate.HasValue
            &&
            x.ReleaseDate.Value.CompareTo(query.Value) <= 0
        )
    )) { }
}