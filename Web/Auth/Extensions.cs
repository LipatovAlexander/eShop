using Infrastructure.Data;
using Infrastructure.Dependencies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Abstractions;
using static Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults;

namespace Auth;

public static class Extensions
{
    public static IServiceCollection AddAuthenticationAndJwt (this IServiceCollection sc)
    {
        sc.AddAuthentication(configureOptions =>
        {
            configureOptions.DefaultAuthenticateScheme = AuthenticationScheme;
            configureOptions.DefaultChallengeScheme = AuthenticationScheme;
        })
            .AddJwtBearer(options => { options.ClaimsIssuer = AuthenticationScheme; });
        return sc;
    }

    public static IServiceCollection ConfigureIdentityOptions (this IServiceCollection services)
    {
        services.Configure<IdentityOptions>(options =>
        {
            options.ClaimsIdentity.UserNameClaimType = OpenIddictConstants.Claims.Name;
            options.ClaimsIdentity.UserIdClaimType = OpenIddictConstants.Claims.Subject;
            options.ClaimsIdentity.RoleClaimType = OpenIddictConstants.Claims.Role;
            options.ClaimsIdentity.EmailClaimType = OpenIddictConstants.Claims.Email;
        });

        return services;
    }

    public static IServiceCollection AddDbContext (this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddApplicationDbContext(connectionString, options =>
        {
            options.UseOpenIddict();
        });

        return services;
    }

    public static IServiceCollection AddOpenIddictServer (this IServiceCollection services,
        IWebHostEnvironment environment)
    {
        services
            .AddOpenIddict()
            .AddCore(options =>
            {
                options
                    .UseEntityFrameworkCore()
                    .UseDbContext<ApplicationDbContext>();
            })
            .AddServer(options =>
            {
                options
                    .AcceptAnonymousClients()
                    .AllowPasswordFlow()
                    .AllowRefreshTokenFlow();

                options
                    .SetTokenEndpointUris(Routes.Token);

                // Register the ASP.NET Core host and configure the ASP.NET Core-specific options.
                var cfg = options.UseAspNetCore();
                if (environment.IsDevelopment() || environment.IsStaging())
                {
                    cfg.DisableTransportSecurityRequirement();
                }

                cfg.EnableTokenEndpointPassthrough();

                options
                    .AddDevelopmentEncryptionCertificate()
                    .AddDevelopmentSigningCertificate();
            }).AddValidation(options =>
            {
                options.UseAspNetCore();
                options.UseLocalServer();
            });
        return services;
    }
}
