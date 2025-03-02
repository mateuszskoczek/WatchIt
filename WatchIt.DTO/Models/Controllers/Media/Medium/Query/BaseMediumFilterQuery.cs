using Microsoft.AspNetCore.Mvc;
using Refit;
using WatchIt.DTO.Models.Controllers.Media.Medium.Filters;
using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.Media.Medium.Query;

public class BaseMediumFilterQuery<T> : IFilterQuery<T> where T : Database.Model.Media.Medium
{
    #region PROPERTIES

    [FromQuery(Name = "title")]
    public string? Title { get; set; }

    [FromQuery(Name = "original_title")]
    [AliasAs("original_title")]
    public string? OriginalTitle { get; set; }

    [FromQuery(Name = "description")]
    public string? Description { get; set; }

    [FromQuery(Name = "release_date_from")]
    [AliasAs("release_date_from")]
    public DateOnly? ReleaseDateFrom { get; set; }

    [FromQuery(Name = "release_date_to")]
    [AliasAs("release_date_to")]
    public DateOnly? ReleaseDateTo { get; set; }
    
    [AliasAs("genre")]
    [FromQuery(Name = "genre")]
    public IEnumerable<short>? Genres { get; set; }

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

    [FromQuery(Name = "view_count_last_24_hours_from")]
    [AliasAs("view_count_last_24_hours_from")]
    public long? ViewCountLast24HoursFrom { get; set; }

    [FromQuery(Name = "view_count_last_24_hours_from")]
    [AliasAs("view_count_last_24_hours_from")]
    public long? ViewCountLast24HoursTo { get; set; }

    [FromQuery(Name = "view_count_last_week_from")]
    [AliasAs("view_count_last_week_from")]
    public long? ViewCountLastWeekFrom { get; set; }

    [FromQuery(Name = "view_count_last_week_to")]
    [AliasAs("view_count_last_week_to")]
    public long? ViewCountLastWeekTo { get; set; }

    [FromQuery(Name = "view_count_last_month_from")]
    [AliasAs("view_count_last_month_from")]
    public long? ViewCountLastMonthFrom { get; set; }

    [FromQuery(Name = "view_count_last_month_to")]
    [AliasAs("view_count_last_month_to")]
    public long? ViewCountLastMonthTo { get; set; }

    [FromQuery(Name = "view_count_last_year_from")]
    [AliasAs("view_count_last_year_from")]
    public long? ViewCountLastYearFrom { get; set; }

    [FromQuery(Name = "view_count_last_year_to")]
    [AliasAs("view_count_last_year_to")]
    public long? ViewCountLastYearTo { get; set; }

    #endregion
    
    
    
    #region PUBLIC METHODS

    public virtual IEnumerable<Filter<T>> GetFilters() =>
    [
        new MediumTitleFilter<T>(Title),
        new MediumOriginalTitleFilter<T>(OriginalTitle),
        new MediumDescriptionFilter<T>(Description),
        new MediumReleaseDateFromFilter<T>(ReleaseDateFrom),
        new MediumReleaseDateToFilter<T>(ReleaseDateTo),
        new MediumGenresFilter<T>(Genres),
        new MediumRatingAverageFromFilter<T>(RatingAverageFrom),
        new MediumRatingAverageToFilter<T>(RatingAverageTo),
        new MediumRatingCountFromFilter<T>(RatingCountFrom),
        new MediumRatingCountToFilter<T>(RatingCountTo),
        new MediumViewCountLast24HoursFromFilter<T>(ViewCountLast24HoursFrom),
        new MediumViewCountLast24HoursToFilter<T>(ViewCountLast24HoursTo),
        new MediumViewCountLastWeekFromFilter<T>(ViewCountLastWeekFrom),
        new MediumViewCountLastWeekToFilter<T>(ViewCountLastWeekTo),
        new MediumViewCountLastMonthFromFilter<T>(ViewCountLastMonthFrom),
        new MediumViewCountLastMonthToFilter<T>(ViewCountLastMonthTo),
        new MediumViewCountLastYearFromFilter<T>(ViewCountLastYearFrom),
        new MediumViewCountLastYearToFilter<T>(ViewCountLastYearTo),
    ];

    #endregion
}