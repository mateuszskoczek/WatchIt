using System.Security.Cryptography;
using System.Text;
using SimpleToolkit.Extensions;
using WatchIt.DTO.Models.Controllers.Accounts;

namespace WatchIt.WebAPI.Helpers;

public static class PasswordHelpers
{
    #region CONSTANTS

    private const string RandomPasswordCharacters = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm1234567890!@#$%^&*()-_=+[{]};:'\"\\|,<.>/?";

    #endregion
    
    
    
    #region PUBLIC METHODS

    public static PasswordData GeneratePasswordData(string password)
    {
        string leftSalt = StringExtensions.CreateRandom(20, RandomPasswordCharacters);
        string rightSalt = StringExtensions.CreateRandom(20, RandomPasswordCharacters);
        byte[] hash = ComputeHash(password, leftSalt, rightSalt);
        return new PasswordData
        {
            LeftSalt = leftSalt,
            RightSalt = rightSalt,
            PasswordHash = hash,
        };
    }

    public static byte[] ComputeHash(string password, string leftSalt, string rightSalt)
    {
        string stringToHash = $"{leftSalt}{password}{rightSalt}";
        byte[] encodedString = Encoding.UTF8.GetBytes(stringToHash);
        byte[] hash = SHA512.HashData(encodedString);
        return hash;
    }

    public static bool ValidatePassword(string password, PasswordData passwordData)
    {
        byte[] checkedHash = ComputeHash(password, passwordData.LeftSalt, passwordData.RightSalt);
        byte[] actualHash = passwordData.PasswordHash;
        bool result = checkedHash.SequenceEqual(actualHash);
        return result;
    }
    
    #endregion
}