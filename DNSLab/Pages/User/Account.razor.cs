namespace DNSLab.Pages.User;
partial class Account
{
    Modal DeleteAccountModal;

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
