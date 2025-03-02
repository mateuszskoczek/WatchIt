using Microsoft.AspNetCore.Mvc;
using WatchIt.DTO.Models.Controllers.Genres.Genre.Filters;
using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.Genres.Genre;

public class GenreFilterQuery : IFilterQuery<Database.Model.Genres.Genre>
{
    #region PROPERTIES
    
    [FromQuery(Name = "name")]
    public string? Name { get; set; }
    
    #endregion
    
    
    
    #region PUBLIC METHODS

    public IEnumerable<Filter<Database.Model.Genres.Genre>> GetFilters() =>
    [
        new GenreNameFilter(Name),
    ];

    #endregion
}