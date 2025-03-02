using Microsoft.AspNetCore.Mvc;
using Refit;
using WatchIt.DTO.Models.Controllers.Media.Medium.Filters;
using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.Media.Medium.Query;

public class MediumUserRatedFilterQuery<T> : IFilterQuery<T> where T : Database.Model.Media.Medium
{
    #region PROPERTIES
    
    [FromQuery(Name = "rating_user_rating_from")]
    [AliasAs("rating_user_rating_from")]
    public byte? RatingUserRatingFrom { get; set; }

    [FromQuery(Name = "rating_user_rating_to")]
    [AliasAs("rating_user_rating_to")]
    public byte? RatingUserRatingTo { get; set; }

    [FromQuery(Name = "rating_user_date_from")]
    [AliasAs("rating_user_date_from")]
    public DateOnly? RatingUserDateFrom { get; set; }

    [FromQuery(Name = "rating_user_date_to")]
    [AliasAs("rating_user_date_to")]
    public DateOnly? RatingUserDateTo { get; set; }
    
    
    [FromRoute(Name = "id")] 
    public long AccountId { get; set; }

    #endregion
    
    
    
    #region PUBLIC METHODS

    public IEnumerable<Filter<T>> GetFilters() => 
    [
        new MediumRatingUserRatingFromFilter<T>(RatingUserRatingFrom, AccountId),
        new MediumRatingUserRatingToFilter<T>(RatingUserRatingTo, AccountId),
        new MediumRatingUserDateFromFilter<T>(RatingUserDateFrom, AccountId),
        new MediumRatingUserDateToFilter<T>(RatingUserDateTo, AccountId)
    ];

    #endregion
}