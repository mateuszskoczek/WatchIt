using Microsoft.AspNetCore.Mvc;
using WatchIt.Common.Query;

namespace WatchIt.Common.Model.Genres;

public class GenreQueryParameters : QueryParameters<GenreResponse>
{
    #region PROPERTIES
    
    [FromQuery(Name = "name")]
    public string? Name { get; set; }
    
    [FromQuery(Name = "description")]
    public string? Description { get; set; }
    
    #endregion
    
    
    
    #region PUBLIC METHODS

    public override bool IsMeetingConditions(GenreResponse item) =>
    (
        TestString(item.Name, Name)
        &&
        TestString(item.Description, Description)
    );

    #endregion
}