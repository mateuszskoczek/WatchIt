using WatchIt.Database.Model.Media;
using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.Media.Medium.Filters;

public record MediumTypeFilter : Filter<Database.Model.Media.Medium>
{
    public MediumTypeFilter(MediumType? query) : base(x =>
    (
        query == null
        ||
        x.Type == query
    )) { }
}