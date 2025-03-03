namespace WatchIt.DTO.Models.Controllers.Accounts.AccountPassword;

public class AccountPasswordRequest : IPasswordEditRequest
{
    #region PROPERTIES
    
    public string OldPassword { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string PasswordConfirmation { get; set; } = null!;

    #endregion
}