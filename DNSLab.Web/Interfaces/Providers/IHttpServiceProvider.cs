namespace DNSLab.Web.Interfaces.Providers
{
    public interface IHttpServiceProvider
    {
        Task CheckTokenAsync();
        Task<TResponse?> Get<TResponse>(string url, bool checkToken = true);
        Task<TResponse?> Post<T, TResponse>(string url, T data);
        Task<TResponse?> Post<TResponse>(string url);
        Task<TResponse?> Put<T, TResponse>(string url, T data);
        Task<TResponse?> Put<TResponse>(string url);
        Task<TResponse?> Delete<TResponse>(string url);
    }
}
