using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace WatchIt.WebAPI.Services.Tokens;

public static class SecurityTokenDescriptorExtensions
{
    #region PUBLIC METHODS

    public static string ToJwtString(this SecurityTokenDescriptor tokenDescriptor)
    {
        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        handler.InboundClaimTypeMap.Clear();

        SecurityToken token = handler.CreateToken(tokenDescriptor);

        return handler.WriteToken(token);
    }
    
    #endregion
}