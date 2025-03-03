using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.Media.Medium.Filters;

public record MediumViewCountLastMonthToFilter<T> : Filter<T> where T : Database.Model.Media.Medium
{
    public MediumViewCountLastMonthToFilter(long? query) : base(x =>
    (
        query == null
        ||
        x.ViewCounts
         .Where(y => y.Date >= DateOnly.FromDateTime(DateTime.Now.AddMonths(-1)))
         .Sum(y => y.ViewCount) <= query
    )) { }
}