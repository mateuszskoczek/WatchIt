using Microsoft.AspNetCore.Mvc;
using WatchIt.DTO.Models.Controllers.Genders.Gender.Filters;
using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.Genders.Gender;

public class GenderFilterQuery : IFilterQuery<Database.Model.Genders.Gender>
{
    #region PROPERTIES
    
    [FromQuery(Name = "name")]
    public string? Name { get; set; }
    
    #endregion
    
    
    
    #region PUBLIC METHODS

    public IEnumerable<Filter<Database.Model.Genders.Gender>> GetFilters() =>
    [
        new GenderNameFilter(Name),
    ];

    #endregion
}