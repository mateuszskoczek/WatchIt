namespace WatchIt.DTO.Models.Controllers.Accounts;

public struct PasswordData
{
    #region PROPERTIES
    
    public required byte[] PasswordHash { get; set; }
    public required string LeftSalt { get; set; }
    public required string RightSalt { get; set; }
    
    #endregion
}