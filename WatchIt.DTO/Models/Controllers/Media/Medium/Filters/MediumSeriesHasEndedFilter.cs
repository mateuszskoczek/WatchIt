using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.Media.Medium.Filters;

public record MediumSeriesHasEndedFilter : Filter<Database.Model.Media.MediumSeries>
{
    public MediumSeriesHasEndedFilter(bool? query) : base(x =>
    (
        query == null
        ||
        x.HasEnded == query
    )) { }
}