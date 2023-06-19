using DNSLab.DTOs.User;

namespace DNSLab.Pages.User;
partial class Account
{
    bool ChangeAccountUsernameModalVisible, ChangeAccountEmailModalVisible = false;

    private UserInfo userInfo;
    ChangeEmailDTO changeEmail;
    string? Username;
    private bool ResentButtonClicked = false;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            userInfo = await _AccountRepository.Get();
            changeEmail = new ChangeEmailDTO { Email = userInfo.Email };
            Username = userInfo!.Username;
            await this.InvokeAsync(() => StateHasChanged());
        }
    }

    async Task AcceptChangeAccountUsername()
    {
        if (await _AccountRepository.UpdateUsername(Username))
        {
            ChangeAccountUsernameModalVisible = false;
            Snackbar.Add($"نام کاربری شما با موفقیت {(String.IsNullOrWhiteSpace(Username) ? $"حذف گردید و از این پس با نام کاربری {userInfo.Email} میتوانید وارد سیستم شوید" : "تغییر یافت")}.", MudBlazor.Severity.Info);
        }
    }

    async Task AcceptDeactivateAccount()
    {
        if (await _AccountRepository.DeactivateAccount())
        {
            await _AuthService.Logout();
            _NavigationManager.NavigateTo("");
            await this.InvokeAsync(() => StateHasChanged());
            Snackbar.Add("حساب کاربری شما با موفقیت از سیستم حذف گردید. امیدواریم بازم برگدید :)", MudBlazor.Severity.Info);
        }
    }

    private async Task AcceptChangeEmail()
    {
        if (await _AccountRepository.ChangeEmail(changeEmail))
        {
            Snackbar.Add("ایمیل جدید ذخیره شد و ایمیل تایید ارسال گردید", MudBlazor.Severity.Success);

            if (userInfo.Email.Trim().ToLower() != changeEmail.Email.Trim().ToLower())
                userInfo.IsEmailApproved = false;

            userInfo.Email = changeEmail.Email;
            ChangeAccountEmailModalVisible = false;
        }
    }

    private async Task ResendActivationLink()
    {
        if (await _AccountRepository.ResendConfirmEmailToken())
        {
            ResentButtonClicked = true;
        }
    }
}
