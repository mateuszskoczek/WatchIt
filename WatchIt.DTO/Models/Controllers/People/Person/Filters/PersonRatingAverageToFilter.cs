using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.People.Person.Filters;

public record PersonRatingAverageToFilter : Filter<Database.Model.People.Person>
{
    public PersonRatingAverageToFilter(decimal? query) : base(x =>
    (
        query == null
        ||
        (
            x.Roles.SelectMany(y => y.Ratings).Any()
            &&
            (decimal)x.Roles.SelectMany(y => y.Ratings).Average(y => y.Rating) <= query
        )
    )) { }
}