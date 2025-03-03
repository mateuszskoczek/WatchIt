using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.People.Person.Filters;

public record PersonBirthDateFromFilter : Filter<Database.Model.People.Person>
{
    public PersonBirthDateFromFilter(DateOnly? query) : base(x =>
    (
        query == null
        ||
        x.BirthDate >= query
    )) { }
}