using Microsoft.AspNetCore.Mvc;
using WatchIt.Common.Query;

namespace WatchIt.Common.Model.Accounts;

public class AccountQueryParameters : QueryParameters<AccountResponse>
{
    #region PROPERTIES
    
    [FromQuery(Name = "username")]
    public string? Username { get; set; }
    
    [FromQuery(Name = "email")]
    public string? Email { get; set; }
    
    [FromQuery(Name = "description")]
    public string? Description { get; set; }
    
    [FromQuery(Name = "gender_id")]
    public short? GenderId { get; set; }

    [FromQuery(Name = "last_active")]
    public DateOnly? LastActive { get; set; }

    [FromQuery(Name = "last_active_from")]
    public DateOnly? LastActiveFrom { get; set; }

    [FromQuery(Name = "last_active_to")]
    public DateOnly? LastActiveTo { get; set; }

    [FromQuery(Name = "creation_date")]
    public DateOnly? CreationDate { get; set; }

    [FromQuery(Name = "creation_date_from")]
    public DateOnly? CreationDateFrom { get; set; }

    [FromQuery(Name = "creation_date_to")]
    public DateOnly? CreationDateTo { get; set; }
    
    [FromQuery(Name = "is_admin")]
    public bool? IsAdmin { get; set; }

    #endregion
    
    
    
    #region PRIVATE METHODS

    protected override bool IsMeetingConditions(AccountResponse item) =>
    (
        TestStringWithRegex(item.Username, Username)
        &&
        TestStringWithRegex(item.Email, Email)
        &&
        TestStringWithRegex(item.Description, Description)
        &&
        Test(item.Gender?.Id, GenderId)
        &&
        TestComparable(item.LastActive, LastActive, LastActiveFrom, LastActiveTo)
        &&
        TestComparable(item.CreationDate, CreationDate, CreationDateFrom, CreationDateTo)
        &&
        Test(item.IsAdmin, IsAdmin)
    );

    #endregion
}