using Microsoft.AspNetCore.Mvc;
using WatchIt.Common.Query;

namespace WatchIt.Common.Model.Series;

public class SeriesRatedQueryParameters : QueryParameters<SeriesRatedResponse>
{
    #region PROPERTIES

    [FromQuery(Name = "title")]
    public string? Title { get; set; }

    [FromQuery(Name = "original_title")]
    public string? OriginalTitle { get; set; }

    [FromQuery(Name = "description")]
    public string? Description { get; set; }

    [FromQuery(Name = "release_date")]
    public DateOnly? ReleaseDate { get; set; }

    [FromQuery(Name = "release_date_from")]
    public DateOnly? ReleaseDateFrom { get; set; }

    [FromQuery(Name = "release_date_to")]
    public DateOnly? ReleaseDateTo { get; set; }

    [FromQuery(Name = "length")]
    public short? Length { get; set; }

    [FromQuery(Name = "length_from")]
    public short? LengthFrom { get; set; }

    [FromQuery(Name = "length_to")]
    public short? LengthTo { get; set; }

    [FromQuery(Name = "has_ended")]
    public bool? HasEnded { get; set; }

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
    
    [FromQuery(Name = "genre")]
    public IEnumerable<short>? Genres { get; set; }
    
    [FromQuery(Name = "user_rating")]
    public decimal? UserRating { get; set; }

    [FromQuery(Name = "user_rating_from")]
    public decimal? UserRatingFrom { get; set; }

    [FromQuery(Name = "user_rating_to")]
    public decimal? UserRatingTo { get; set; }

    [FromQuery(Name = "user_rating_date")]
    public DateOnly? UserRatingDate { get; set; }

    [FromQuery(Name = "user_rating_date_from")]
    public DateOnly? UserRatingDateFrom { get; set; }

    [FromQuery(Name = "user_rating_date_to")]
    public DateOnly? UserRatingDateTo { get; set; }

    #endregion
    
    
    
    #region PRIVATE METHODS

    protected override bool IsMeetingConditions(SeriesRatedResponse item) =>
    (
        TestStringWithRegex(item.Title, Title)
        &&
        TestStringWithRegex(item.OriginalTitle, OriginalTitle)
        &&
        TestStringWithRegex(item.Description, Description)
        &&
        TestComparable(item.ReleaseDate, ReleaseDate, ReleaseDateFrom, ReleaseDateTo)
        &&
        TestComparable(item.Length, Length, LengthFrom, LengthTo)
        &&
        Test(item.HasEnded, HasEnded)
        &&
        TestComparable(item.Rating.Average, RatingAverage, RatingAverageFrom, RatingAverageTo)
        &&
        TestComparable(item.Rating.Count, RatingCount, RatingCountFrom, RatingCountTo)
        &&
        TestContains(item.Genres.Select(x => x.Id), Genres)
        &&
        TestComparable((decimal)item.UserRating, UserRating, UserRatingFrom, UserRatingTo)
        &&
        TestComparable(item.UserRatingDate, UserRatingDate, UserRatingDateFrom, UserRatingDateTo)
    );

    #endregion
}