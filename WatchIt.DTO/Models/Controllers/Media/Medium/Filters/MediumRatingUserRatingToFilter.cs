using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.Media.Medium.Filters;

public record MediumRatingUserRatingToFilter<T> : Filter<T> where T : Database.Model.Media.Medium
{
    public MediumRatingUserRatingToFilter(byte? query, long accountId) : base(x =>
    (
        query == null
        ||
        (
            x.Ratings.Any(x => x.AccountId == accountId)
            &&
            x.Ratings.First(x => x.AccountId == accountId).Rating <= query
        )
    )) { }
}