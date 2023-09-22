using System.Text;
using Assessment3.Server.Application.Common.Authentication;
using Assessment3.Server.Infrastructure.Common.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Assessment3.Server.Infrastructure.Extensions;

public static class AuthenticationService
{
    public static IServiceCollection AddAuthenticationService(
        this IServiceCollection services, ConfigurationManager configuration)
    {
        var jwtSettings = new JwtSetting();
        configuration.Bind(JwtSetting.SectioName, jwtSettings);
        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.Secret))
            });
        return services;
    }
}