using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.People.Person.Filters;

public record PersonRatingCountToFilter : Filter<Database.Model.People.Person>
{
    public PersonRatingCountToFilter(long? query) : base(x =>
    (
        query == null
        ||
        x.Roles.SelectMany(y => y.Ratings).Count() <= query
    )) { }
}