using DNSLab.Interfaces.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Reflection;

namespace DNSLab.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("sitemap.txt")]
    [ApiController]
    public class SiteMapController : ControllerBase
    {
        private readonly IPageRepository _pageRepository;
        public SiteMapController(IPageRepository pageRepository)
        {
            _pageRepository = pageRepository;
        }

        private static string[] _ExceptedPages = new string[]
        {
            "https://dnslab.link/user/confirmemail/{token}",
            "https://dnslab.link/user/resetpassword/{token}",
            "https://dnslab.link/{pagetype}/{*url}",
            "https://dnslab.link/changelog/{*url}",
            "https://dnslab.link/tag/{tag}",
            "https://dnslab.link/404",
            "https://dnslab.link/500",
            "https://dnslab.link/503",
            "https://dnslab.link/user/logout",
            "https://dnslab.link/transaction/callback",
            "https://dnslab.link/transaction/tip",
        };

        public async Task<IActionResult> Get()
        {
            var pages = Assembly.GetExecutingAssembly().ExportedTypes.Where(p =>
               p.IsSubclassOf(typeof(ComponentBase)));

            var assemblyRoutes = pages.Where(x => x.CustomAttributes.Any(x => x.AttributeType == typeof(Microsoft.AspNetCore.Components.RouteAttribute)) &&
                                                   !x.CustomAttributes.Any(x => x.AttributeType == typeof(AuthorizeAttribute)))
                .Select(x => x.CustomAttributes.FirstOrDefault(x => x.AttributeType == typeof(Microsoft.AspNetCore.Components.RouteAttribute))!.ConstructorArguments[0].Value).ToList();

            var apiRoutes = await _pageRepository.GetAllPagesURL();
            assemblyRoutes.AddRange(apiRoutes.Select(x => "/" + x));

            string routes = String.Empty;

            foreach (var assemblyroute in assemblyRoutes)
            {
                string routeWithDomain = $"https://dnslab.link{assemblyroute}";
                if (!_ExceptedPages.Contains(routeWithDomain.ToLower()))
                    routes += routeWithDomain + Environment.NewLine;
            }

            return Ok(routes);
        }
    }
}
