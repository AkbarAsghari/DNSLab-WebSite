using Microsoft.JSInterop;

namespace DNSLab.Helper.Extensions
{
    public static class JSRuntimeExtensions
    {
        public static ValueTask<object> SetLocalStorage(this IJSRuntime jS, string key, string content)
            => jS.InvokeAsync<object>("localStorage.setItem", key, content);

        public static ValueTask<string> GetFromLocalStorage(this IJSRuntime jS, string key)
            => jS.InvokeAsync<string>("localStorage.getItem", key);

        public static ValueTask<object> RemoveFromLocalStorage(this IJSRuntime jS, string key)
            => jS.InvokeAsync<object>("localStorage.removeItem", key);
    }
}
