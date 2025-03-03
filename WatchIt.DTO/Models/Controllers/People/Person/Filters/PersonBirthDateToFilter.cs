using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.People.Person.Filters;

public record PersonBirthDateToFilter : Filter<Database.Model.People.Person>
{
    public PersonBirthDateToFilter(DateOnly? query) : base(x =>
    (
        query == null
        ||
        x.BirthDate <= query
    )) { }
}