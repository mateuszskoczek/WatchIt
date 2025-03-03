using Microsoft.AspNetCore.Mvc;
using Refit;
using WatchIt.DTO.Models.Controllers.People.Person.Filters;
using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.People.Person.Query;

public class PersonUserRatedFilterQuery : IFilterQuery<Database.Model.People.Person>
{
    #region PROPERTIES
    
    [FromQuery(Name = "rating_user_average_from")]
    [AliasAs("rating_user_average_from")]
    public decimal? RatingUserAverageFrom { get; set; }

    [FromQuery(Name = "rating_user_average_to")]
    [AliasAs("rating_user_average_to")]
    public decimal? RatingUserAverageTo { get; set; }

    [FromQuery(Name = "rating_user_count_from")]
    [AliasAs("rating_user_count_from")]
    public long? RatingUserCountFrom { get; set; }

    [FromQuery(Name = "rating_user_count_to")]
    [AliasAs("rating_user_count_to")]
    public long? RatingUserCountTo { get; set; }

    [FromQuery(Name = "rating_user_last_rating_date_from")]
    [AliasAs("rating_user_last_rating_date_from")]
    public DateOnly? RatingUserLastRatingDateFrom { get; set; }

    [FromQuery(Name = "rating_user_last_rating_date_to")]
    [AliasAs("rating_user_last_rating_date_to")]
    public DateOnly? RatingUserLastRatingDateTo { get; set; }
    
    
    [FromRoute(Name = "id")] 
    public long AccountId { get; set; }
    
    #endregion



    #region PUBLIC METHODS

    public IEnumerable<Filter<Database.Model.People.Person>> GetFilters() =>
    [
        new PersonRatingUserAverageFromFilter(RatingUserAverageFrom, AccountId),
        new PersonRatingUserAverageToFilter(RatingUserAverageTo, AccountId),
        new PersonRatingUserCountFromFilter(RatingUserCountFrom, AccountId),
        new PersonRatingUserCountToFilter(RatingUserCountTo, AccountId),
        new PersonRatingUserLastRatingDateFromFilter(RatingUserLastRatingDateFrom, AccountId),
        new PersonRatingUserLastRatingDateToFilter(RatingUserLastRatingDateTo, AccountId),
    ];

    #endregion
}