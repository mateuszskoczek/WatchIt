using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.Media.Medium.Filters;

public record MediumRatingUserDateToFilter<T> : Filter<T> where T : Database.Model.Media.Medium
{
    public MediumRatingUserDateToFilter(DateOnly? query, long accountId) : base(x =>
    (
        query == null
        ||
        (
            x.Ratings.Any(x => x.AccountId == accountId)
            &&
            x.Ratings.First(x => x.AccountId == accountId).Date <= query.Value.ToDateTime(new TimeOnly(23, 59))
        )
    )) { }
}