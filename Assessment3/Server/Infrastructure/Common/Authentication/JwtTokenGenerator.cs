using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Assessment3.Server.Application.Common.Authentication;
using Assessment3.Server.Application.Common.Services;
using Assessment3.Server.Domain.Common;
using Assessment3.Shared.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Assessment3.Server.Infrastructure.Common.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IDateTimeProvider _dateTimeProvider;

    private readonly JwtSetting _jwtSetting;
    public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSetting> jwtOptions)
    {
        _dateTimeProvider = dateTimeProvider;
        _jwtSetting = jwtOptions.Value;
    }
    public string GenerateToken(User user)
    {
        try
        {
            var signedCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtSetting.Secret)),
            SecurityAlgorithms.HmacSha256
        );
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
            new Claim(JwtRegisteredClaimNames.FamilyName,  user.LastName),
            new Claim(JwtRegisteredClaimNames.Jti,  Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email,  user.Email),
            new Claim(ClaimTypes.Role,  user.Role),
        };

            var securityToken = new JwtSecurityToken(
                issuer: _jwtSetting.Issuer,
                audience: _jwtSetting.Audience,
                expires: _dateTimeProvider.UtcNow.AddMinutes(_jwtSetting.ExpiryMinutes),
                claims: claims,
                signingCredentials: signedCredentials);
            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
        catch (Exception ex)
        {

            throw;
        }
    }
}