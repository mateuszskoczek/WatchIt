namespace WatchIt.DTO.Models.Controllers.Accounts.AccountEmail;

public class AccountEmailRequest
{
    #region PROPERTIES
    
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;

    #endregion
}