using Microsoft.AspNetCore.Mvc;
using WatchIt.Common.Query;

namespace WatchIt.Common.Model.Persons;

public class PersonRatedQueryParameters : QueryParameters<PersonRatedResponse>
{
    #region PROPERTIES
    
    [FromQuery(Name = "name")]
    public string? Name { get; set; }

    [FromQuery(Name = "full_name")]
    public string? FullName { get; set; }

    [FromQuery(Name = "description")]
    public string? Description { get; set; }

    [FromQuery(Name = "birth_date")]
    public DateOnly? BirthDate { get; set; }

    [FromQuery(Name = "birth_date_from")]
    public DateOnly? BirthDateFrom { get; set; }

    [FromQuery(Name = "birth_date_to")]
    public DateOnly? BirthDateTo { get; set; }

    [FromQuery(Name = "death_date")]
    public DateOnly? DeathDate { get; set; }

    [FromQuery(Name = "death_date_from")]
    public DateOnly? DeathDateFrom { get; set; }

    [FromQuery(Name = "death_date_to")]
    public DateOnly? DeathDateTo { get; set; }

    [FromQuery(Name = "gender_id")]
    public short? GenderId { get; set; }

    [FromQuery(Name = "rating_average")]
    public decimal? RatingAverage { get; set; }

    [FromQuery(Name = "rating_average_from")]
    public decimal? RatingAverageFrom { get; set; }

    [FromQuery(Name = "rating_average_to")]
    public decimal? RatingAverageTo { get; set; }

    [FromQuery(Name = "rating_count")]
    public long? RatingCount { get; set; }

    [FromQuery(Name = "rating_count_from")]
    public long? RatingCountFrom { get; set; }

    [FromQuery(Name = "rating_count_to")]
    public long? RatingCountTo { get; set; }

    [FromQuery(Name = "user_rating_average")]
    public decimal? UserRatingAverage { get; set; }

    [FromQuery(Name = "user_rating_average_from")]
    public decimal? UserRatingAverageFrom { get; set; }

    [FromQuery(Name = "user_rating_average_to")]
    public decimal? UserRatingAverageTo { get; set; }

    [FromQuery(Name = "user_rating_count")]
    public long? UserRatingCount { get; set; }

    [FromQuery(Name = "user_rating_count_from")]
    public long? UserRatingCountFrom { get; set; }

    [FromQuery(Name = "user_rating_count_to")]
    public long? UserRatingCountTo { get; set; }

    [FromQuery(Name = "user_rating_date")]
    public DateOnly? UserRatingLastDate { get; set; }

    [FromQuery(Name = "user_rating_date_from")]
    public DateOnly? UserRatingLastDateFrom { get; set; }

    [FromQuery(Name = "user_rating_date_to")]
    public DateOnly? UserRatingLastDateTo { get; set; }
    
    #endregion
    
    
    
    #region PUBLIC METHODS
    
    protected override bool IsMeetingConditions(PersonRatedResponse item) =>
    (
        TestStringWithRegex(item.Name, Name)
        &&
        TestStringWithRegex(item.FullName, FullName)
        &&
        TestStringWithRegex(item.Description, Description)
        &&
        TestComparable(item.BirthDate, BirthDate, BirthDateFrom, BirthDateTo)
        &&
        TestComparable(item.DeathDate, DeathDate, DeathDateFrom, DeathDateTo)
        &&
        Test(item.Gender?.Id, GenderId)
        &&
        TestComparable(item.Rating.Average, RatingAverage, RatingAverageFrom, RatingAverageTo)
        &&
        TestComparable(item.Rating.Count, RatingCount, RatingCountFrom, RatingCountTo)
        &&
        TestComparable(item.UserRating.Average, UserRatingAverage, UserRatingAverageFrom, UserRatingAverageTo)
        &&
        TestComparable(item.UserRating.Count, UserRatingCount, UserRatingCountFrom, UserRatingCountTo)
        &&
        TestComparable(item.UserRatingLastDate, UserRatingLastDate, UserRatingLastDateFrom, UserRatingLastDateTo)
    );
    
    #endregion
}