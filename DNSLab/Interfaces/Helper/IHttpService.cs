using DNSLab.DTOs.RestAPI;
using DNSLab.Helper.HttpService;

namespace DNSLab.Interfaces.Helper
{
    public interface IHttpService
    {
        Task<HttpResponseWraper<TResponse>> Get<TResponse>(string url);
        Task<HttpResponseWraper<object>> Post<T>(string url, T data);
        Task<HttpResponseWraper<TResponse>> Post<T, TResponse>(string url, T data);
        Task<HttpResponseWraper<TResponse>> Put<T, TResponse>(string url, T data);
        Task<HttpResponseWraper<TResponse>> Put<TResponse>(string url);
        Task<HttpResponseWraper<TResponse>> Delete< TResponse>(string url);
        Task<HttpResponseWraper<object>> Put<T>(string url, T data);
    }
}
