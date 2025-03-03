using Microsoft.AspNetCore.Mvc;
using WatchIt.DTO.Models.Controllers.Roles.RoleActorType.Filters;
using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.Roles.RoleActorType;

public class RoleActorTypeFilterQuery : IFilterQuery<Database.Model.Roles.RoleActorType>
{
    #region PROPERTIES
    
    [FromQuery(Name = "name")]
    public string? Name { get; set; }
    
    #endregion
    
    
    
    #region PUBLIC METHODS

    public IEnumerable<Filter<Database.Model.Roles.RoleActorType>> GetFilters() =>
    [
        new RoleActorTypeNameFilter(Name),
    ];

    #endregion
}