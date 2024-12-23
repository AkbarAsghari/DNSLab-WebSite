using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using DNSLab.Web.AppSettings;
using DNSLab.Web.Exceptions;
using DNSLab.Web.Interfaces.Providers;

namespace DNSLab.Web.Providers
{
    public class HttpServiceProvider(HttpClient _HttpClient, HttpResponseExceptionHander _HttpResponseExceptionHander, ITokenProvider _TokenProvider) : IHttpServiceProvider
    {
        string _BaseUrl = GlobalSettings.APIBaseAddress;

        private async Task<T?> GenerateHttpResponseWraper<T>(HttpResponseMessage httpResponse)
        {
            if (!httpResponse.IsSuccessStatusCode)
            {
                await _HttpResponseExceptionHander.HandlerExceptionAsync(httpResponse);
                return default;
            }
            else
            {
                var responseString = await httpResponse.Content.ReadAsStringAsync();

                T? response;
                if (typeof(T) == typeof(String))
                {
                    response = (T)(object)responseString;
                }
                else
                {
                    response = JsonSerializer.Deserialize<T>(responseString,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                }

                return response;
            }
        }

        public async Task CheckTokenAsync()
        {
            var token = await _TokenProvider.GetTokenAsync();
            if (String.IsNullOrEmpty(token))
            {
                var refreshToken = await _TokenProvider.GetRefreshTokenAsync();
                if (!String.IsNullOrEmpty(refreshToken))
                {
                    var newToken = await GenerateHttpResponseWraper<AuthUserDTO?>(await _HttpClient.PostAsync($"{_BaseUrl}Account/GenerateTokenWithRefreshTokenAsync", GenerateStringContentFromObject<AuthUserDTO>(new AuthUserDTO
                    {
                        RefreshToken = refreshToken
                    })));

                    if (newToken != null)
                    {
                        await _TokenProvider.SetTokenAsync(newToken);
                        token = newToken.Token;
                    }
                    else
                    {
                        await _TokenProvider.DeleteTokenAsync();
                    }
                }
            }

            _HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
        }

        public StringContent GenerateStringContentFromObject<T>(T data)
        {
            return new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
        }

        public async Task<TResponse?> Delete<TResponse>(string url)
        {
            await CheckTokenAsync();
            return await GenerateHttpResponseWraper<TResponse>(await _HttpClient.DeleteAsync(_BaseUrl + url));
        }

        public async Task<TResponse?> Get<TResponse>(string url)
        {
            await CheckTokenAsync();
            return await GenerateHttpResponseWraper<TResponse>(await _HttpClient.GetAsync(_BaseUrl + url));

        }

        public async Task<TResponse?> Post<T, TResponse>(string url, T data)
        {
            await CheckTokenAsync();
            return await GenerateHttpResponseWraper<TResponse>(await _HttpClient.PostAsync(_BaseUrl + url, GenerateStringContentFromObject(data)));
        }

        public async Task<TResponse?> Post<TResponse>(string url)
        {
            await CheckTokenAsync();
            return await GenerateHttpResponseWraper<TResponse>(await _HttpClient.PostAsync(_BaseUrl + url, null));
        }

        public async Task<TResponse?> Put<T, TResponse>(string url, T data)
        {
            await CheckTokenAsync();
            return await GenerateHttpResponseWraper<TResponse>(await _HttpClient.PutAsync(_BaseUrl + url, GenerateStringContentFromObject(data)));
        }
        public async Task<TResponse?> Put<TResponse>(string url)
        {
            await CheckTokenAsync();
            return await GenerateHttpResponseWraper<TResponse>(await _HttpClient.PutAsync(_BaseUrl + url, null));
        }
    }
}
