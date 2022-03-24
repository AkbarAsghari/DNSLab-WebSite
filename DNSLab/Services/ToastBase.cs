using DNSLab.Enums;
using DNSLab.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace DNSLab.Services
{
    public class ToastBase : ComponentBase, IDisposable
    {
        [Inject] IStringLocalizer<Resource> _localizer { get; set; }
        [Inject] ToastService ToastService { get; set; }

        protected string Heading { get; set; }
        protected string Message { get; set; }
        protected bool IsVisible { get; set; }
        protected string BackgroundCssClass { get; set; }
        protected string IconImgSrc { get; set; }

        protected override void OnInitialized()
        {
            ToastService.OnShow += ShowToast;
            ToastService.OnHide += HideToast;
        }

        private void ShowToast(string message, ToastLevel level)
        {
            BuildToastSettings(level, message);
            IsVisible = true;
            this.InvokeAsync(() => StateHasChanged());
        }

        private void HideToast()
        {
            IsVisible = false;
            this.InvokeAsync(() => StateHasChanged());
        }

        private void BuildToastSettings(ToastLevel level, string message)
        {
            switch (level)
            {
                case ToastLevel.Info:
                    BackgroundCssClass = "bg-info";
                    IconImgSrc = "../images/Icons/letter_icon.svg";
                    Heading = _localizer["Info"];
                    break;
                case ToastLevel.Success:
                    BackgroundCssClass = "bg-success";
                    IconImgSrc = "../images/Icons/confirm_icon.svg";
                    Heading = _localizer["Success"];
                    break;
                case ToastLevel.Warning:
                    BackgroundCssClass = "bg-warning";
                    IconImgSrc = "../images/Icons/warn_icon.svg";
                    Heading = _localizer["Warning"];
                    break;
                case ToastLevel.Error:
                    BackgroundCssClass = "bg-danger";
                    IconImgSrc = "../images/Icons/error_icon.svg";
                    Heading = _localizer["Error"];
                    break;
            }

            Message = message;
        }

        public void Dispose()
        {
            ToastService.OnShow -= ShowToast;
        }
    }
}
