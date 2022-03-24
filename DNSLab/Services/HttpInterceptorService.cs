using DNSLab.DTOs.RestAPI;
using DNSLab.Helper.Exceptions;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net;
using System.Text.Json;
using Toolbelt.Blazor;

namespace DNSLab.Services
{
    public class HttpInterceptorService
    {
        private readonly HttpClientInterceptor _interceptor;
        private readonly NavigationManager _navManager;
        private readonly ToastService toastService;
        private readonly ApplicationExceptions applicationExceptions;

        public HttpInterceptorService(HttpClientInterceptor interceptor, NavigationManager navManager, ToastService toastService, ApplicationExceptions applicationExceptions)
        {
            _interceptor = interceptor;
            _navManager = navManager;
            this.toastService = toastService;
            this.applicationExceptions = applicationExceptions;
        }
        public void RegisterEvent() => _interceptor.AfterSendAsync += InterceptResponse;
        private async Task InterceptResponse(object sender, HttpClientInterceptorEventArgs e)
        {
            if (!e.Response.IsSuccessStatusCode)
            {
                var statusCode = e.Response.StatusCode;
                await ShowToastMessageAsync(e.Response);
                switch (statusCode)
                {
                    case HttpStatusCode.NotFound:
                        _navManager.NavigateTo("/404");
                        break;
                    case HttpStatusCode.Unauthorized:
                        if (_navManager.Uri.ToLower().EndsWith("/user/auth"))
                        {
                            toastService.ShowToast("username or password is wrong", Enums.ToastLevel.Error);
                        }
                        else
                        {
                            _navManager.NavigateTo("/user/auth");
                            toastService.ShowToast("please first login", Enums.ToastLevel.Info);
                        }
                        break;
                    case HttpStatusCode.BadRequest:
                    case HttpStatusCode.Conflict:
                        break;
                    default:
                        _navManager.NavigateTo("/500");
                        break;
                }



            }
        }

        private async Task ShowToastMessageAsync(HttpResponseMessage response)
        {
            string content = await response.Content!.ReadAsStringAsync();
            if (String.IsNullOrEmpty(content))
                return;

            var error = JsonSerializer.Deserialize<ErrorDTO>(content!, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            var existMessage = applicationExceptions.GetExceptions().FirstOrDefault(x => x.ExceptionStr == error!.Error);
            if (existMessage != null)
            {
                toastService.ShowToast(existMessage.NormalMessage, existMessage.Level);
            }
        }

        public void DisposeEvent() => _interceptor.AfterSendAsync -= InterceptResponse;
    }
}
