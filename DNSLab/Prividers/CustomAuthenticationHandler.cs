using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Encodings.Web;

namespace DNSLab.Prividers
{
    public class CustomAuthenticationHandler(IOptionsMonitor<CustomAuthOptions> options, ILoggerFactory logger, UrlEncoder encoder, IHttpContextAccessor httpContextAccessor)
    : AuthenticationHandler<CustomAuthOptions>(options, logger, encoder)
    {
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            return Task.Factory.StartNew<AuthenticateResult>(() => AuthenticateResult.Success(new AuthenticationTicket(new ClaimsPrincipal(new GenericIdentity("CustomIdentity")), "CustomAuthenticationScheme")));
        }
    }
}
