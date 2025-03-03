using Microsoft.AspNetCore.Mvc;
using Refit;
using WatchIt.DTO.Models.Controllers.Roles.Role.Filters;
using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.Roles.Role.Query;

public abstract class BaseRoleFilterQuery<T> : IFilterQuery<T> where T : Database.Model.Roles.Role
{
    #region PROPERTIES
    
    [FromQuery(Name = "person_id")]
    [AliasAs("person_id")]
    public long? PersonId { get; set; }
    
    [FromQuery(Name = "medium_id")]
    [AliasAs("medium_id")]
    public long? MediumId { get; set; }
    
    #endregion



    #region PUBLIC METHODS

    public virtual IEnumerable<Filter<T>> GetFilters() =>
    [
        new RolePersonIdFilter<T>(PersonId),
        new RoleMediumIdFilter<T>(MediumId),
    ];

    #endregion
}