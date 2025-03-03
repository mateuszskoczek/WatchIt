using Microsoft.AspNetCore.Mvc;
using Refit;
using WatchIt.DTO.Models.Controllers.People.Person.Filters;
using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.People.Person.Query;

public class PersonFilterQuery : IFilterQuery<Database.Model.People.Person>
{
    #region PROPERTIES
    
    [FromQuery(Name = "name")]
    public string? Name { get; set; }

    [FromQuery(Name = "full_name")]
    [AliasAs("full_name")]
    public string? FullName { get; set; }

    [FromQuery(Name = "description")]
    public string? Description { get; set; }

    [FromQuery(Name = "birth_date_from")]
    [AliasAs("birth_date_from")]
    public DateOnly? BirthDateFrom { get; set; }

    [FromQuery(Name = "birth_date_to")]
    [AliasAs("birth_date_to")]
    public DateOnly? BirthDateTo { get; set; }

    [FromQuery(Name = "death_date_from")]
    [AliasAs("death_date_from")]
    public DateOnly? DeathDateFrom { get; set; }

    [FromQuery(Name = "death_date_to")]
    [AliasAs("death_date_to")]
    public DateOnly? DeathDateTo { get; set; }

    [FromQuery(Name = "gender_id")]
    [AliasAs("gender_id")]
    public short? GenderId { get; set; }

    [FromQuery(Name = "rating_average_from")]
    [AliasAs("rating_average_from")]
    public decimal? RatingAverageFrom { get; set; }

    [FromQuery(Name = "rating_average_to")]
    [AliasAs("rating_average_to")]
    public decimal? RatingAverageTo { get; set; }

    [FromQuery(Name = "rating_count_from")]
    [AliasAs("rating_count_from")]
    public long? RatingCountFrom { get; set; }

    [FromQuery(Name = "rating_count_to")]
    [AliasAs("rating_count_to")]
    public long? RatingCountTo { get; set; }
    
    #endregion



    #region PUBLIC METHODS

    public IEnumerable<Filter<Database.Model.People.Person>> GetFilters() =>
    [
        new PersonNameFilter(Name),
        new PersonFullNameFilter(FullName),
        new PersonDescriptionFilter(Description),
        new PersonBirthDateFromFilter(BirthDateFrom),
        new PersonBirthDateToFilter(BirthDateTo),
        new PersonDeathDateFromFilter(DeathDateFrom),
        new PersonDeathDateToFilter(DeathDateTo),
        new PersonGenderIdFilter(GenderId),
        new PersonRatingAverageFromFilter(RatingAverageFrom),
        new PersonRatingAverageToFilter(RatingAverageTo),
        new PersonRatingCountFromFilter(RatingCountFrom),
        new PersonRatingCountToFilter(RatingCountTo),
    ];

    #endregion
}