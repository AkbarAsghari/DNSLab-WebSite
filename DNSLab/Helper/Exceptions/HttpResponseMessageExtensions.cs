using DNSLab.DTOs.RestAPI;
using DNSLab.Helper.Exceptions;
using DNSLab.Services;
using Microsoft.AspNetCore.Components;
using System.Net;
using System.Text.Json;

namespace DNSLab.Helper.Exceptions
{
    public class HttpResponseExceptionHander
    {

        private readonly NavigationManager _navManager;
        private readonly ToastService _toastService;
        private readonly ApplicationExceptions _applicationExceptions;

        public HttpResponseExceptionHander(NavigationManager navManager, ToastService toastService, ApplicationExceptions applicationException)
        {
            _navManager = navManager;
            _toastService = toastService;
            _applicationExceptions = applicationException;
        }

        public async Task HandlerExceptionAsync(HttpResponseMessage httpResponseMessage)
        {
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                var statusCode = httpResponseMessage.StatusCode;
                await ShowToastMessageAsync(httpResponseMessage);
                switch (statusCode)
                {
                    case HttpStatusCode.NotFound:
                        _navManager.NavigateTo("/404");
                        break;
                    case HttpStatusCode.Unauthorized:
                        if (!_navManager.Uri.ToLower().EndsWith("/user/auth"))
                        {
                            _navManager.NavigateTo("/user/auth");
                            _toastService.ShowToast("please first login", Enums.ToastLevel.Info);
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
            var existMessage = _applicationExceptions.GetExceptions().FirstOrDefault(x => x.ExceptionStr == error!.Error);
            if (existMessage != null)
            {
                _toastService.ShowToast(existMessage.NormalMessage, existMessage.Level);
            }
        }
    }
}
