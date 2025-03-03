using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.People.Person.Filters;

public record PersonDeathDateFromFilter : Filter<Database.Model.People.Person>
{
    public PersonDeathDateFromFilter(DateOnly? query) : base(x =>
    (
        query == null
        ||
        x.DeathDate >= query
    )) { }
}