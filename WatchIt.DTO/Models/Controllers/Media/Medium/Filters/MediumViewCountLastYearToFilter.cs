using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.Media.Medium.Filters;

public record MediumViewCountLastYearToFilter<T> : Filter<T> where T : Database.Model.Media.Medium
{
    public MediumViewCountLastYearToFilter(long? query) : base(x =>
    (
        query == null
        ||
        x.ViewCounts
         .Where(y => y.Date >= DateOnly.FromDateTime(DateTime.Now.AddYears(-1)))
         .Sum(y => y.ViewCount) <= query
    )) { }
}