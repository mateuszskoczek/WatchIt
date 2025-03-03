using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.People.Person.Filters;

public record PersonRatingUserAverageToFilter : Filter<Database.Model.People.Person>
{
    public PersonRatingUserAverageToFilter(decimal? query, long accountId) : base(x =>
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
                      .Average(y => y.Rating) <= query
        )
    )) { }
}