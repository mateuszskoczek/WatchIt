using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.Media.Medium.Filters;

public record MediumMovieBudgetToFilter : Filter<Database.Model.Media.MediumMovie>
{
    public MediumMovieBudgetToFilter(decimal? query) : base(x =>
    (
        query == null
        ||
        x.Budget <= query
    )) { }
}