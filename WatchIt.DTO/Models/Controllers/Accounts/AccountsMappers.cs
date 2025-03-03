using WatchIt.Database.Model.Accounts;
using WatchIt.DTO.Models.Controllers.Accounts.Account;
using WatchIt.DTO.Models.Controllers.Accounts.AccountBackgroundPicture;
using WatchIt.DTO.Models.Controllers.Accounts.AccountEmail;
using WatchIt.DTO.Models.Controllers.Accounts.AccountPassword;
using WatchIt.DTO.Models.Controllers.Accounts.AccountProfileInfo;
using WatchIt.DTO.Models.Controllers.Accounts.AccountUsername;
using WatchIt.DTO.Models.Controllers.Genders;
using WatchIt.DTO.Models.Generics.Image;

namespace WatchIt.DTO.Models.Controllers.Accounts;

public static class AccountsMappers
{
    #region PUBLIC METHODS
    
    #region Account

    public static Database.Model.Accounts.Account ToEntity(this AccountRequest request, Func<string, PasswordData> passwordGenFunc)
    {
        PasswordData generatedPassword = passwordGenFunc(request.Password);
        return new Database.Model.Accounts.Account
        {
            Username = request.Username,
            Email = request.Email,
            Password = generatedPassword.PasswordHash,
            LeftSalt = generatedPassword.LeftSalt,
            RightSalt = generatedPassword.RightSalt,
        };
    }
    
    public static AccountResponse ToResponse(this Database.Model.Accounts.Account account) => new AccountResponse
    {
        Id = account.Id,
        Username = account.Username,
        Email = account.Email,
        IsAdmin = account.IsAdmin,
        JoinDate = account.JoinDate,
        ActiveDate = account.ActiveDate,
        Description = account.Description,
        Gender = account.Gender?.ToResponse(),
        ProfilePicture = account.ProfilePicture?.ToResponse(),
    };

    public static void UpdateActiveDate(this Database.Model.Accounts.Account account)
    {
        account.ActiveDate = DateTimeOffset.UtcNow;
    }
    
    #endregion
    
    #region AccountUsername

    public static void UpdateWithRequest(this Database.Model.Accounts.Account account, AccountUsernameRequest request)
    {
        account.Username = request.Username;
    }
    
    #endregion
    
    #region AccountEmail

    public static void UpdateWithRequest(this Database.Model.Accounts.Account account, AccountEmailRequest request)
    {
        account.Email = request.Email;
    }
    
    #endregion
    
    #region AccountPassword

    public static void UpdateWithRequest(this Database.Model.Accounts.Account account, AccountPasswordRequest request, Func<string, PasswordData> passwordGenFunc)
    {
        PasswordData generatedPassword = passwordGenFunc(request.Password);
        account.Password = generatedPassword.PasswordHash;
        account.LeftSalt = generatedPassword.LeftSalt;
        account.RightSalt = generatedPassword.RightSalt;
    }
    
    #endregion
    
    #region AccountProfileInfo

    public static void UpdateWithRequest(this Database.Model.Accounts.Account account, AccountProfileInfoRequest request)
    {
        account.Description = request.Description;
        account.GenderId = request.GenderId;
    }

    public static AccountProfileInfoRequest ToProfileInfoRequest(this AccountResponse response) => new AccountProfileInfoRequest
    {
        Description = response.Description,
        GenderId = response.Gender?.Id
    };
    
    #endregion
    
    #region AccountProfilePicture

    public static AccountProfilePicture ToEntity(this ImageRequest request, long accountId) => new AccountProfilePicture
    {
        AccountId = accountId,
        Image = request.Image,
        MimeType = request.MimeType,
    };

    #endregion
    
    #region AccountBackgroundPicture

    public static Database.Model.Accounts.AccountBackgroundPicture ToEntity(this AccountBackgroundPictureRequest request, long accountId) => new Database.Model.Accounts.AccountBackgroundPicture
    {
        AccountId = accountId,
        BackgroundId = request.Id,
    };

    public static void UpdateWithRequest(this Database.Model.Accounts.AccountBackgroundPicture entity, AccountBackgroundPictureRequest request)
    {
        entity.BackgroundId = request.Id;
    }

    #endregion
    
    #region AccountRefreshToken

    public static AccountRefreshToken CreateAccountRefreshTokenEntity(Guid token, long accountId, DateTimeOffset expirationDate, bool isExtendable) => new AccountRefreshToken
    {
        Token = token,
        AccountId = accountId,
        ExpirationDate = expirationDate,
        IsExtendable = isExtendable,
    };

    public static void UpdateExpirationDate(this AccountRefreshToken entity, DateTimeOffset expirationDate)
    {
        entity.ExpirationDate = expirationDate;
    }
    
    #endregion
    
    #region AccountFollow
    
    public static AccountFollow CreateAccountFollowEntity(long accountFollowerId, long accountFollowedId) => new AccountFollow
    {
        FollowerId = accountFollowerId,
        FollowedId = accountFollowedId,
    };
    
    #endregion
    
    #region PasswordData

    public static PasswordData GetPasswordData(this Database.Model.Accounts.Account account) => new PasswordData
    {
        PasswordHash = account.Password,
        LeftSalt = account.LeftSalt,
        RightSalt = account.RightSalt,
    };

    #endregion

    #endregion
}