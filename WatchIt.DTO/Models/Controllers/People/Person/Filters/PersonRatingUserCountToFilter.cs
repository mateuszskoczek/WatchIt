using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.People.Person.Filters;

public record PersonRatingUserCountToFilter : Filter<Database.Model.People.Person>
{
    public PersonRatingUserCountToFilter(long? query, long accountId) : base(x =>
    (
        query == null
        ||
        x.Roles
         .SelectMany(y => y.Ratings)
         .Count(y => y.AccountId == accountId) <= query
    )) { }
}