using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.Media.Medium.Filters;

public record MediumRatingAverageToFilter<T> : Filter<T> where T : Database.Model.Media.Medium
{
    public MediumRatingAverageToFilter(decimal? query) : base(x =>
    (
        query == null
        ||
        (
            x.Ratings.Any()
            &&
            (decimal)x.Ratings.Average(y => y.Rating) <= query
        )
    )) { }
}