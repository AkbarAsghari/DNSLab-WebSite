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
                 path.ToLower().Contains("about");
        }
    }
}
