namespace WatchIt.DTO.Models.Controllers.Accounts;

public interface IPasswordEditRequest
{
    #region PROPERTIES
    
    string Password { get; set; }
    string PasswordConfirmation { get; set; }
    
    #endregion
}