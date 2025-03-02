using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.People.Person.Filters;

public record PersonRatingUserAverageFromFilter : Filter<Database.Model.People.Person>
{
    public PersonRatingUserAverageFromFilter(decimal? query, long accountId) : base(x =>
    (
        query == null
        ||
        (
            x.Roles
             .SelectMany(y => y.Ratings)
             .Any(y => y.AccountId == accountId)
            &&
            (decimal)x.Roles
                      .SelectMany(y => y.Ratings)
                      .Where(y => y.AccountId == accountId)
                      .Average(y => y.Rating) >= query
        )
    )) { }
}