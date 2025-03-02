using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.People.Person.Filters;

public record PersonRatingCountFromFilter : Filter<Database.Model.People.Person>
{
    public PersonRatingCountFromFilter(long? query) : base(x =>
    (
        query == null
        ||
        x.Roles.SelectMany(y => y.Ratings).Count() >= query
    )) { }
}