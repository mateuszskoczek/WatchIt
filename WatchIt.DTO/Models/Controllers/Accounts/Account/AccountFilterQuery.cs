using Microsoft.AspNetCore.Mvc;
using WatchIt.DTO.Models.Controllers.Accounts.Account.Filters;
using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.Accounts.Account;

public class AccountFilterQuery : IFilterQuery<Database.Model.Accounts.Account>
{
    #region PROPERTIES
    
    [FromQuery(Name = "username")]
    public string? Username { get; set; }
    
    [FromQuery(Name = "email")]
    public string? Email { get; set; }
    
    [FromQuery(Name = "is_admin")]
    public bool? IsAdmin { get; set; }

    [FromQuery(Name = "active_date_from")]
    public DateOnly? ActiveDateFrom { get; set; }

    [FromQuery(Name = "active_date_to")]
    public DateOnly? ActiveDateTo { get; set; }

    [FromQuery(Name = "join_date_from")]
    public DateOnly? JoinDateFrom { get; set; }

    [FromQuery(Name = "join_date_to")]
    public DateOnly? JoinDateTo { get; set; }
    
    [FromQuery(Name = "description")]
    public string? Description { get; set; }
    
    [FromQuery(Name = "gender_id")]
    public short? GenderId { get; set; }
    
    #endregion
    
    
    
    #region PUBLIC METHODS

    public IEnumerable<Filter<Database.Model.Accounts.Account>> GetFilters() =>
    [
        new AccountUsernameFilter(Username),
        new AccountEmailFilter(Email),
        new AccountIsAdminFilter(IsAdmin),
        new AccountActiveDateFromFilter(ActiveDateFrom),
        new AccountActiveDateToFilter(ActiveDateTo),
        new AccountJoinDateFromFilter(JoinDateFrom),
        new AccountJoinDateToFilter(JoinDateTo),
        new AccountDescriptionFilter(Description),
        new AccountGenderIdFilter(GenderId),
    ];

    #endregion
}