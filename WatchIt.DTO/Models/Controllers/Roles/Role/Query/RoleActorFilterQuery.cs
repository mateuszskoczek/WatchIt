using Microsoft.AspNetCore.Mvc;
using Refit;
using WatchIt.Database.Model.Roles;
using WatchIt.DTO.Models.Controllers.Roles.Role.Filters;
using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.Roles.Role.Query;

public class RoleActorFilterQuery : BaseRoleFilterQuery<RoleActor>, IRoleFilterQuery
{
    #region PROPERTIES

    [FromQuery(Name = "type_id")]
    [AliasAs("type_id")]
    public short? TypeId { get; set; }

    [FromQuery(Name = "name")]
    [AliasAs("name")]
    public string? Name { get; set; }

    #endregion
    
    
    
    #region PUBLIC METHODS

    public override IEnumerable<Filter<RoleActor>> GetFilters() => base.GetFilters()
                                                                       .Append(new RoleActorTypeIdFilter(TypeId))
                                                                       .Append(new RoleActorNameFilter(Name));

    #endregion
}