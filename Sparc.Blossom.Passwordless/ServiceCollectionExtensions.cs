using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Sparc.Blossom.Server.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Sparc.Blossom.Passwordless;

namespace Sparc.Blossom.Authentication;

public static class ServiceCollectionExtensions
{
    public static WebApplicationBuilder AddBlossomPasswordlessAuthentication<TUser>(this WebApplicationBuilder builder)
        where TUser : BlossomUser, new()
    {
        builder.Services.AddScoped<AuthenticationStateProvider, BlossomPasswordlessAuthenticator<TUser>>()
            .AddScoped<BlossomPasswordlessAuthenticator<TUser>>()
            .AddScoped(typeof(IBlossomAuthenticator), typeof(BlossomPasswordlessAuthenticator<TUser>));

        return builder;
    }

    public static IApplicationBuilder UseBlossomPasswordlessAuthentication(this IApplicationBuilder app)
    {
        app.UseCookiePolicy(new() { MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.Strict });
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseMiddleware<BlossomDefaultAuthenticatorMiddleware>();

        return app;
    }
}
