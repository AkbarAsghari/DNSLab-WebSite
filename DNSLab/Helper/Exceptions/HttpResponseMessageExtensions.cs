using DNSLab.DTOs.RestAPI;
using DNSLab.Helper.Exceptions;
using DNSLab.Resources;
using DNSLab.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;
using System.Net;
using System.Text.Json;

namespace DNSLab.Helper.Exceptions
{
    public class HttpResponseExceptionHander
    {

        private readonly NavigationManager _navManager;
        private readonly ISnackbar _Snackbar;
        private readonly ApplicationExceptions _applicationExceptions;
        private readonly IStringLocalizer<Resource> _localizer;

        public HttpResponseExceptionHander(NavigationManager navManager, ISnackbar snackbar, ApplicationExceptions applicationException, IStringLocalizer<Resource> localizer)
        {
            _navManager = navManager;
            _Snackbar = snackbar;
            _applicationExceptions = applicationException;
            _localizer = localizer;
        }

        public async Task HandlerExceptionAsync(HttpResponseMessage httpResponseMessage)
        {
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                if (await ShowToastMessageAsync(httpResponseMessage))
                    return;

                var statusCode = httpResponseMessage.StatusCode;

                switch (statusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        if (!_navManager.Uri.ToLower().EndsWith("/user/auth"))
                            _navManager.NavigateTo($"/user/auth");
                        break;
                    case HttpStatusCode.Forbidden:
                    case HttpStatusCode.BadRequest:
                    case HttpStatusCode.Conflict:
                        break;
                    case HttpStatusCode.TooManyRequests:

                        break;
                    case HttpStatusCode.NotFound:
                        _navManager.NavigateTo("/404", true);
                        break;
                    case HttpStatusCode.InternalServerError:
                        _navManager.NavigateTo("/500", true);
                        break;
                    case HttpStatusCode.ServiceUnavailable:
                        _navManager.NavigateTo("/503", true);
                        break;
                }
            }
        }
        private async Task<bool> ShowToastMessageAsync(HttpResponseMessage response)
        {
            string content = await response.Content!.ReadAsStringAsync();
            if (String.IsNullOrEmpty(content))
                return false;

            if (response.StatusCode == HttpStatusCode.TooManyRequests)
            {
                var tooManyRequest = JsonSerializer.Deserialize<TooManyRequestDTO>(content!, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                _Snackbar.Add($"تعداد درخواست بیش از حد مجاز میباشد (تعداد مجاز {tooManyRequest!.MaximumAllowed} در {tooManyRequest.Per} میباشد لطفا {tooManyRequest.Remain} ثانیه صبر کنید).",
                    Severity.Error,
                    configure: config =>
                    {
                        config.DuplicatesBehavior = SnackbarDuplicatesBehavior.Prevent;
                    }, key: tooManyRequest.Error);

                return true;
            }

            var error = JsonSerializer.Deserialize<ErrorDTO>(content!, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            var existMessage = _applicationExceptions.GetExceptions().FirstOrDefault(x => x.ExceptionStr == error!.Error);
            if (existMessage != null)
            {
                _Snackbar.Add(existMessage.NormalMessage, existMessage.Severity, configure: String.IsNullOrEmpty(existMessage.ActionUrl) ? null : config =>
                {
                    config.Action = existMessage.ActionContent;
                    config.ActionColor = Color.Secondary;
                    config.ActionVariant = Variant.Filled;
                    config.Onclick = snackbar =>
                    {
                        _navManager.NavigateTo(existMessage.ActionUrl);
                        return Task.CompletedTask;
                    };
                });
                return true;
            }
            return false;
        }
    }
}
