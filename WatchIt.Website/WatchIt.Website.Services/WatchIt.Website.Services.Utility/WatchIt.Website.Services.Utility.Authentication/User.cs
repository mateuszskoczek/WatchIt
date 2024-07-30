namespace WatchIt.Website.Services.Utility.Authentication;

public class User
{
    #region PROPERTIES
    
    public required long Id { get; init; }
    public required string Username { get; init; }
    public required string Email { get; init; }
    public required bool IsAdmin { get; init; }
    
    #endregion
}