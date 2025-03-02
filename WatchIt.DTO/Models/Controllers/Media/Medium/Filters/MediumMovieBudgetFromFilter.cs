using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.Media.Medium.Filters;

public record MediumMovieBudgetFromFilter : Filter<Database.Model.Media.MediumMovie>
{
    public MediumMovieBudgetFromFilter(decimal? query) : base(x =>
    (
        query == null
        ||
        x.Budget >= query
    )) { }
}