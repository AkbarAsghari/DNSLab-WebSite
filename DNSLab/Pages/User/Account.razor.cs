using DNSLab.DTOs.User;

namespace DNSLab.Pages.User;
partial class Account
{
    Modal DeleteAccountModal, ChangeAccountUsernameModal;

    private UserInfo userInfo;
    string? Username;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            userInfo = await _AccountRepository.Get();
            Username = userInfo!.Username;
        }
    }

    async Task AcceptChangeAccountUsername()
    {
        if (await _AccountRepository.UpdateUsername(Username))
        {
            await ChangeAccountUsernameModal.Close();
            _ToastService.ShowToast($"نام کاربری شما با موفقیت {(String.IsNullOrWhiteSpace(Username) ? $"حذف گردید و از این پس با نام کاربری {userInfo.Email} میتوانید وارد سیستم شوید" : "تغییر یافت")}.", ToastLevel.Info);
        }
    }

    async Task AcceptDeactivateAccount()
    {
        if (await _AccountRepository.DeactivateAccount())
        {
            await _AuthService.Logout();
            _NavigationManager.NavigateTo("");
            await this.InvokeAsync(() => StateHasChanged());
            _ToastService.ShowToast("حساب کاربری شما با موفقیت از سیستم حذف گردید. امیدواریم بازم برگدید :)", ToastLevel.Info);
        }
    }
}
