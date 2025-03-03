using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.Media.Medium.Filters;

public record MediumRatingCountToFilter<T> : Filter<T> where T : Database.Model.Media.Medium
{
    public MediumRatingCountToFilter(long? query) : base(x =>
    (
        query == null
        ||
        x.Ratings.Count() <= query
    )) { }
}