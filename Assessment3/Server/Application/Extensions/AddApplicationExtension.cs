using System.Reflection;
using Assessment3.Server.Application.Behaviors;
using Assessment3.Server.Application.Configuration;
using FluentValidation;
using MediatR;

namespace Assessment3.Server.Application.Extensions;

public static class AddApplicationExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.Configure<JwtIdentityOption>(configuration.GetSection(JwtIdentityOption.SectionName));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }
}