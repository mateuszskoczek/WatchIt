using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.People.Person.Filters;

public record PersonRatingUserLastRatingDateFromFilter : Filter<Database.Model.People.Person>
{
    public PersonRatingUserLastRatingDateFromFilter(DateOnly? query, long accountId) : base(x =>
    (
        query == null
        ||
        (
            x.Roles
             .SelectMany(y => y.Ratings)
             .Any(y => y.AccountId == accountId)
            &&
            x.Roles
             .SelectMany(y => y.Ratings)
             .Where(y => y.AccountId == accountId)
             .Max(y => y.Date) >= query.Value.ToDateTime(new TimeOnly(0, 0))
        )
    )) { }
}