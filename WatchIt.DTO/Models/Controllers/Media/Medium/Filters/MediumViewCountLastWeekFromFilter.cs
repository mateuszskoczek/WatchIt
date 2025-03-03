using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.Media.Medium.Filters;

public record MediumViewCountLastWeekFromFilter<T> : Filter<T> where T : Database.Model.Media.Medium
{
    public MediumViewCountLastWeekFromFilter(long? query) : base(x =>
    (
        query == null
        ||
        x.ViewCounts
         .Where(y => y.Date >= DateOnly.FromDateTime(DateTime.Now.AddDays(-7)))
         .Sum(y => y.ViewCount) >= query
    )) { }
}