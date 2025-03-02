using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.People.Person.Filters;

public record PersonGenderIdFilter : Filter<Database.Model.People.Person>
{
    public PersonGenderIdFilter(short? query) : base(x =>
    (
        query == null
        ||
        x.GenderId == query
    )) { }
}