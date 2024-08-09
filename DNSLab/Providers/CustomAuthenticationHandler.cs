using ApexCharts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;

namespace DNSLab.Providers
{
    public class CustomAuthenticationHandler(AuthenticationStateProvider authStateProvider, IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder)
     : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
    {
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var authState = await authStateProvider.GetAuthenticationStateAsync();
            if (authState.User.Identity?.IsAuthenticated == true)
            {
                return AuthenticateResult.Success(new AuthenticationTicket(authState.User, Scheme.Name));
            }
            else
            {
                return AuthenticateResult.NoResult();
            }
        }

        protected override Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            Response.Redirect("/user/auth");
            return Task.CompletedTask;
        }
    }
}
