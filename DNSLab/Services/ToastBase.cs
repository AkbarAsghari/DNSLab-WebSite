using Bit.BlazorUI;
using DNSLab.Enums;
using DNSLab.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace DNSLab.Services
{
    public class ToastBase : ComponentBase, IDisposable
    {
        [Inject] ToastService ToastService { get; set; }

        protected string Message { get; set; }
        protected BitMessageBarType MessageType { get; set; }
        protected bool IsVisible { get; set; } = false;

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
                    MessageType = BitMessageBarType.Info;
                    break;
                case ToastLevel.Success:
                    MessageType = BitMessageBarType.Success;
                    break;
                case ToastLevel.Warning:
                    MessageType = BitMessageBarType.Warning;
                    break;
                case ToastLevel.Error:
                    MessageType = BitMessageBarType.SevereWarning;
                    break;
            }

            Message = message;
        }

        public void Dispose()
        {
            ToastService.OnShow -= ShowToast;
        }


        public void Close()
        {
            IsVisible = false;
        }
    }
}
