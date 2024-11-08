using Microsoft.AspNetCore.Mvc;
using WatchIt.Common.Query;

namespace WatchIt.Common.Model.Genres;

public class GenreQueryParameters : QueryParameters<GenreResponse>
{
    #region PROPERTIES
    
    [FromQuery(Name = "name")]
    public string? Name { get; set; }
    
    #endregion
    
    
    
    #region PRIVATE METHODS

    protected override bool IsMeetingConditions(GenreResponse item) =>
    (
        TestStringWithRegex(item.Name, Name)
    );

    #endregion
}