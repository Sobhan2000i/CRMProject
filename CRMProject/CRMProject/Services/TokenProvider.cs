using CRMProject.DTOs.Auth;
using CRMProject.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CRMProject.Services
{
    public sealed partial class TokenProvider(IOptions<JwtAuthOptions> options)
    {

        private readonly JwtAuthOptions _jwtAuthOptions = options.Value;

        public AccessTokenDto Create(TokenRequest tokenRequest)
        {
            return new AccessTokenDto(GenerateAccessToken(tokenRequest), GenerateRefreshToken());
        }

        private string GenerateAccessToken(TokenRequest tokenRequest)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtAuthOptions.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims =
                [
                    new(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub , tokenRequest.UserId),
                    new(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.UniqueName , tokenRequest.UserName)

                ];
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtAuthOptions.ExpirationInMinutes),
                SigningCredentials = credentials,
                Issuer = _jwtAuthOptions.Issuer,
                Audience = _jwtAuthOptions.Audience
            };

            var handler = new JsonWebTokenHandler();

            var accessToken = handler.CreateToken(tokenDescriptor);

            return accessToken;
        }

        private static string GenerateRefreshToken()
        {
            return string.Empty;
        }
    }
}
