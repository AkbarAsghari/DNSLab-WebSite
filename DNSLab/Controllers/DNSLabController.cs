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
    public class DNSLabController : ControllerBase
    {
        private readonly IPageRepository _pageRepository;
        public DNSLabController(IPageRepository pageRepository)
        {
            _pageRepository = pageRepository;
        }

        private static string[] _ExceptedPages = new string[]
        {
            "https://dnslab.ir/user/confirmemail/{token}",
            "https://dnslab.ir/user/resetpassword/{token}",
            "https://dnslab.ir/404",
            "https://dnslab.ir/500",
            "https://dnslab.ir/503",
            "https://dnslab.ir/user/logout",
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
                string routeWithDomain = $"https://dnslab.ir{assemblyroute}";
                if (!_ExceptedPages.Contains(routeWithDomain.ToLower()))
                    routes += routeWithDomain + Environment.NewLine;
            }

            return Ok(routes);
        }
    }
}
