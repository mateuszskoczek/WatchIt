using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.Media.Medium.Filters;

public record MediumRatingAverageFromFilter<T> : Filter<T> where T : Database.Model.Media.Medium
{
    public MediumRatingAverageFromFilter(decimal? query) : base(x =>
    (
        query == null
        ||
        (
            x.Ratings.Any()
            &&
            (decimal)x.Ratings.Average(y => y.Rating) >= query
        )
    )) { }
}