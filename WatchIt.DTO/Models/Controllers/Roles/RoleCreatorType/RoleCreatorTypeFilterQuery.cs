using Microsoft.AspNetCore.Mvc;
using WatchIt.DTO.Models.Controllers.Roles.RoleActorType.Filters;
using WatchIt.DTO.Models.Controllers.Roles.RoleCreatorType.Filters;
using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.Roles.RoleCreatorType;

public class RoleCreatorTypeFilterQuery : IFilterQuery<Database.Model.Roles.RoleCreatorType>
{
    #region PROPERTIES
    
    [FromQuery(Name = "name")]
    public string? Name { get; set; }
    
    #endregion
    
    
    
    #region PUBLIC METHODS

    public IEnumerable<Filter<Database.Model.Roles.RoleCreatorType>> GetFilters() =>
    [
        new RoleCreatorTypeNameFilter(Name),
    ];

    #endregion
}