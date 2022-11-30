using Microsoft.Net.Http.Headers;

namespace Microsoft.AspNetCore.Http;

public static class HttpRequestExtensions
{
    public static bool ShouldRenderAsWithoutAdminMenu(this HttpRequest request)
    {
        var path = request.Path.ToString().ToLower();

        return
             path == "/" ||
             path == "//" ||
             path.ToLower().Contains("api") ||
             path.ToLower().Contains("support") ||
             path.ToLower().Contains("about") ||
             path.ToLower().Contains("article");
    }

    public static bool ShouldRenderStaticMode(this HttpRequest request)
    {
        var agent = GetLoweredUserAgent(request);

        if (agent.Contains("google"))
            return true;

        if (agent.Contains("bing"))
            return true;

        if (agent.Contains("lighthouse"))
            return true;

        return false;
    }

    private static string GetLoweredUserAgent(HttpRequest request)
    {
        var userAgent = request.Headers[HeaderNames.UserAgent].ToString();

        if (string.IsNullOrEmpty(userAgent))
            return string.Empty;

        return userAgent.ToLower();
    }
}
