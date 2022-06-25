using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System.Reflection;

namespace DNSLab.Prividers
{
    public class SiteMapProvider
    {
        private static string[] _ExceptedPages = new string[]
        {
            "https://dnslab.ir/user/confirmemail/{token}",
            "https://dnslab.ir/user/resetpassword/{token}",
            "https://dnslab.ir/404",
            "https://dnslab.ir/500",
            "https://dnslab.ir/503",
            "https://dnslab.ir/user/logout",
        };

        public static async Task Generate(HttpContext context)
        {
            var pages = Assembly.GetExecutingAssembly().ExportedTypes.Where(p =>
                p.IsSubclassOf(typeof(ComponentBase)));

            foreach (var route in pages.Where(x => x.CustomAttributes.Any(x => x.AttributeType == typeof(RouteAttribute)) &&
                                                   !x.CustomAttributes.Any(x => x.AttributeType == typeof(AuthorizeAttribute)))
                .Select(x => x.CustomAttributes.FirstOrDefault(x => x.AttributeType == typeof(RouteAttribute))!.ConstructorArguments[0].Value))
            {
                string routeWithDomain = $"https://dnslab.ir{route}";
                if (!_ExceptedPages.Contains(routeWithDomain.ToLower()))
                    await context.Response.WriteAsync(routeWithDomain + "\n");
            }
        }
    }
}
