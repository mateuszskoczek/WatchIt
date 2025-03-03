using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.People.Person.Filters;

public record PersonRatingAverageFromFilter : Filter<Database.Model.People.Person>
{
    public PersonRatingAverageFromFilter(decimal? query) : base(x =>
    (
        query == null
        ||
        (
            x.Roles.SelectMany(y => y.Ratings).Any()
            &&
            (decimal)x.Roles.SelectMany(y => y.Ratings).Average(y => y.Rating) >= query
        )
    )) { }
}