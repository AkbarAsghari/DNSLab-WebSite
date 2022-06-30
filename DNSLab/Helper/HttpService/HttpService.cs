using DNSLab.DTOs.RestAPI;
using DNSLab.Helper.Exceptions;
using DNSLab.Helper.Extensions;
using DNSLab.Interfaces.Helper;
using DNSLab.Services;
using System.Net;
using System.Text;
using System.Text.Json;

namespace DNSLab.Helper.HttpService
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly HttpResponseExceptionHander httpResponseExceptionHander;
        private JsonSerializerOptions defaultJsonSerializationOption
            = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
        private HttpResponseMessage _httpResponseMessage;

        //#if DEBUG
        private const string BaseAddress = "http://192.168.1.7";
        //#else
        //private const string BaseAddress = "https://api.dnslab.ir";
//#endif
        public HttpService(HttpClient httpClient, HttpResponseExceptionHander httpResponseExceptionHander)
        {
            _httpClient = httpClient;
            this.httpResponseExceptionHander = httpResponseExceptionHander;
        }

        public async Task<HttpResponseWraper<object>> Post<T>(string url, T data)
        {
            url = BaseAddress + url;
            var dataJson = JsonSerializer.Serialize(data);
            var stringContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
            _httpResponseMessage = await _httpClient.PostAsync(url, stringContent);
            await httpResponseExceptionHander.HandlerExceptionAsync(_httpResponseMessage);
            return new HttpResponseWraper<object>(null, _httpResponseMessage.IsSuccessStatusCode, _httpResponseMessage);
        }

        public async Task<HttpResponseWraper<TResponse>> Get<TResponse>(string url)
        {
            url = BaseAddress + url;

            _httpResponseMessage = await _httpClient.GetAsync(url);
            await httpResponseExceptionHander.HandlerExceptionAsync(_httpResponseMessage);

            if (_httpResponseMessage.IsSuccessStatusCode)
            {
                var responseDeserialized = await Deserialize<TResponse>(_httpResponseMessage, defaultJsonSerializationOption);
                return new HttpResponseWraper<TResponse>(responseDeserialized, true, _httpResponseMessage);
            }
            else
            {
                return new HttpResponseWraper<TResponse>(default, false, _httpResponseMessage);
            }
        }

        public async Task<HttpResponseWraper<TResponse>> Put<T, TResponse>(string url, T data)
        {
            url = BaseAddress + url;

            var dataJson = JsonSerializer.Serialize(data);
            var stringContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
            _httpResponseMessage = await _httpClient.PutAsync(url, stringContent);
            await httpResponseExceptionHander.HandlerExceptionAsync(_httpResponseMessage);

            if (_httpResponseMessage.IsSuccessStatusCode)
            {
                var responseDeserialized = await Deserialize<TResponse>(_httpResponseMessage, defaultJsonSerializationOption);
                return new HttpResponseWraper<TResponse>(responseDeserialized, true, _httpResponseMessage);
            }
            else
            {
                return new HttpResponseWraper<TResponse>(default, false, _httpResponseMessage);
            }
        }

        public async Task<HttpResponseWraper<object>> Put<T>(string url, T data)
        {
            url = BaseAddress + url;

            var dataJson = JsonSerializer.Serialize(data);
            var stringContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
            _httpResponseMessage = await _httpClient.PutAsync(url, stringContent);

            return new HttpResponseWraper<object>(null, _httpResponseMessage.IsSuccessStatusCode, _httpResponseMessage);

        }

        public async Task<HttpResponseWraper<TResponse>> Put<TResponse>(string url)
        {
            url = BaseAddress + url;

            _httpResponseMessage = await _httpClient.PutAsync(url, null);
            await httpResponseExceptionHander.HandlerExceptionAsync(_httpResponseMessage);

            if (_httpResponseMessage.IsSuccessStatusCode)
            {
                var responseDeserialized = await Deserialize<TResponse>(_httpResponseMessage, defaultJsonSerializationOption);
                return new HttpResponseWraper<TResponse>(responseDeserialized, true, _httpResponseMessage);
            }
            else
            {
                return new HttpResponseWraper<TResponse>(default, false, _httpResponseMessage);
            }
        }

        public async Task<HttpResponseWraper<TResponse>> Delete<TResponse>(string url)
        {
            url = BaseAddress + url;

            _httpResponseMessage = await _httpClient.DeleteAsync(url);
            await httpResponseExceptionHander.HandlerExceptionAsync(_httpResponseMessage);

            if (_httpResponseMessage.IsSuccessStatusCode)
            {
                var responseDeserialized = await Deserialize<TResponse>(_httpResponseMessage, defaultJsonSerializationOption);
                return new HttpResponseWraper<TResponse>(responseDeserialized, true, _httpResponseMessage);
            }
            else
            {
                return new HttpResponseWraper<TResponse>(default, false, _httpResponseMessage);
            }
        }

        public async Task<HttpResponseWraper<TResponse>> Post<T, TResponse>(string url, T data)
        {
            url = BaseAddress + url;

            var dataJson = JsonSerializer.Serialize(data);
            var stringContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
            _httpResponseMessage = await _httpClient.PostAsync(url, stringContent);
            await httpResponseExceptionHander.HandlerExceptionAsync(_httpResponseMessage);
            if (_httpResponseMessage.IsSuccessStatusCode)
            {
                var responseDeserialized = await Deserialize<TResponse>(_httpResponseMessage, defaultJsonSerializationOption);
                return new HttpResponseWraper<TResponse>(responseDeserialized, true, _httpResponseMessage);
            }
            else
            {
                return new HttpResponseWraper<TResponse>(default, false, _httpResponseMessage);
            }
        }

        private async Task<T> Deserialize<T>(HttpResponseMessage httpResponse, JsonSerializerOptions options)
        {
            var responseString = await httpResponse.Content.ReadAsStringAsync();
            if (typeof(T) == typeof(String))
            {
                return (T)(object)responseString;
            }
            return JsonSerializer.Deserialize<T>(responseString, options);
        }

        public async Task<HttpResponseWraper<TResponse>> Post<TResponse>(string url)
        {
            url = BaseAddress + url;

            _httpResponseMessage = await _httpClient.PostAsync(url,null);
            await httpResponseExceptionHander.HandlerExceptionAsync(_httpResponseMessage);
            if (_httpResponseMessage.IsSuccessStatusCode)
            {
                var responseDeserialized = await Deserialize<TResponse>(_httpResponseMessage, defaultJsonSerializationOption);
                return new HttpResponseWraper<TResponse>(responseDeserialized, true, _httpResponseMessage);
            }
            else
            {
                return new HttpResponseWraper<TResponse>(default, false, _httpResponseMessage);
            }
        }
    }
}
