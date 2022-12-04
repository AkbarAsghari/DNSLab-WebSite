using Microsoft.AspNetCore.Components;

namespace DNSLab.Helper.Extensions
{
    public static class NavigationManagerExtensions
    {
        public static bool ShouldRenderAsWithoutAdminMenu(this NavigationManager navigationManager)
        {
            var path = new Uri(navigationManager.Uri).AbsolutePath.ToLower();

            return
                 path == "/" ||
                 path == "//" ||
                 path.ToLower().Contains("api") ||
                 path.ToLower().Contains("support") ||
                 path.ToLower().Contains("about") ||
                 path.ToLower().Contains("article") ||
                 path.ToLower().Contains("transaction/tip") ||
                 path.ToLower().Contains("transaction/callback");
        }
    }
}
