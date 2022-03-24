using DNSLab.Models.JS;
using Microsoft.JSInterop;

namespace DNSLab.Helper.Exceptions
{
    public static class JSRuntimeExtensions
    {
        public static async Task<BrowserDimension> GetDimensions(this IJSRuntime js)
        {
            return await js.InvokeAsync<BrowserDimension>("getDimensions");
        }
    }
}
