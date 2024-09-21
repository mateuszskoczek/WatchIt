using Microsoft.AspNetCore.Mvc;
using WatchIt.Common.Query;

namespace WatchIt.Common.Model.Series;

public class SeriesQueryParameters : QueryParameters<SeriesResponse>
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

    #endregion
    
    
    
    #region PUBLIC METHODS

    public override bool IsMeetingConditions(SeriesResponse item) =>
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
        TestBoolean(item.HasEnded, HasEnded)
    );

    #endregion
}