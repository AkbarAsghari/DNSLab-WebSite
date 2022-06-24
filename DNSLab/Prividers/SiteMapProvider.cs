using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System.Reflection;

namespace DNSLab.Prividers
{
    public class SiteMapProvider
    {
        public static async Task Generate(HttpContext context)
        {
            var pages = Assembly.GetExecutingAssembly().ExportedTypes.Where(p =>
                p.IsSubclassOf(typeof(ComponentBase)));

            foreach (var route in pages.Where(x => x.CustomAttributes.Any(x => x.AttributeType == typeof(RouteAttribute)) &&
                                                   !x.CustomAttributes.Any(x=>x.AttributeType == typeof(AuthorizeAttribute)))
                .Select(x => x.CustomAttributes.FirstOrDefault(x => x.AttributeType == typeof(RouteAttribute))!.ConstructorArguments[0].Value))
            {
                await context.Response.WriteAsync("https://dnslab.ir" + route + "\n");
            }
        }
    }
}
