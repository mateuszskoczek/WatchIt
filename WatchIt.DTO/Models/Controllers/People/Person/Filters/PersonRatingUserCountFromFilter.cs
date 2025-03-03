using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.People.Person.Filters;

public record PersonRatingUserCountFromFilter : Filter<Database.Model.People.Person>
{
    public PersonRatingUserCountFromFilter(long? query, long accountId) : base(x =>
    (
        query == null
        ||
        x.Roles
         .SelectMany(y => y.Ratings)
         .Count(y => y.AccountId == accountId) >= query
    )) { }
}