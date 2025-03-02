using Microsoft.AspNetCore.Mvc;
using Refit;
using WatchIt.Database.Model.Roles;
using WatchIt.DTO.Models.Controllers.Roles.Role.Filters;
using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.Roles.Role.Query;

public class RoleCreatorFilterQuery : BaseRoleFilterQuery<RoleCreator>, IRoleFilterQuery
{
    #region PROPERTIES

    [FromQuery(Name = "type_id")]
    [AliasAs("type_id")]
    public short? TypeId { get; set; }

    #endregion
    
    
    
    #region PUBLIC METHODS

    public override IEnumerable<Filter<RoleCreator>> GetFilters() => base.GetFilters()
                                                                         .Append(new RoleCreatorTypeIdFilter(TypeId));

    #endregion
}