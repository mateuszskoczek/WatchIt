using Microsoft.AspNetCore.Mvc;

namespace WatchIt.Common.Model.Movies;

public class MovieQueryParameters : QueryParameters<MovieResponse>
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

    [FromQuery(Name = "budget")]
    public decimal? Budget { get; set; }

    [FromQuery(Name = "budget_from")]
    public decimal? BudgetFrom { get; set; }

    [FromQuery(Name = "budget_to")]
    public decimal? BudgetTo { get; set; }

    #endregion
    
    
    
    #region PUBLIC METHODS

    public override bool IsMeetingConditions(MovieResponse item) =>
    (
        TestString(item.Title, Title)
        &&
        TestString(item.OriginalTitle, OriginalTitle)
        &&
        TestString(item.Description, Description)
        &&
        TestComparable(item.ReleaseDate, ReleaseDate, ReleaseDateFrom, ReleaseDateTo)
        &&
        TestComparable(item.Length, Length, LengthFrom, LengthTo)
        &&
        TestComparable(item.Budget, Budget, BudgetFrom, BudgetTo)
    );

    #endregion
}