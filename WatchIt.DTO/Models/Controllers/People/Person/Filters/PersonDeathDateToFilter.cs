using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.People.Person.Filters;

public record PersonDeathDateToFilter : Filter<Database.Model.People.Person>
{
    public PersonDeathDateToFilter(DateOnly? query) : base(x =>
    (
        query == null
        ||
        x.DeathDate <= query
    )) { }
}