using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.Media.Medium.Filters;

public record MediumViewCountLast24HoursToFilter<T> : Filter<T> where T : Database.Model.Media.Medium
{
    public MediumViewCountLast24HoursToFilter(long? query) : base(x =>
    (
        query == null
        ||
        x.ViewCounts
         .Where(y => y.Date >= DateOnly.FromDateTime(DateTime.Now.AddDays(-1)))
         .Sum(y => y.ViewCount) <= query
    )) { }
}