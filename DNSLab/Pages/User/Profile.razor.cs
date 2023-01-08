using DNSLab.DTOs.User;

namespace DNSLab.Pages.User;
partial class Profile
{
    UserInfo userInfo;
    ChangeEmailDTO changeEmail;

    CultureSelector cultureSelector;
    private bool ResentButtonClicked = false;

    Modal ChangeEmailModal { get; set; }


    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            userInfo = await accountRepository.Get();
            changeEmail = new ChangeEmailDTO { Email = userInfo.Email };
            await this.InvokeAsync(() => StateHasChanged());
        }
    }

    private async Task SaveChanges()
    {
        if (await accountRepository.Update(userInfo))
        {
            toastService.ShowToast("تغییرات ذخیره شد", Enums.ToastLevel.Success);
        }

        cultureSelector.Save();
    }

    private async Task ResendActivationLink()
    {
        if (await accountRepository.ResendConfirmEmailToken())
        {
            ResentButtonClicked = true;
        }
    }

    private async Task OpenChangeEmailModal()
    {
        await ChangeEmailModal.Open();
    }

    private async Task AcceptChangeEmail()
    {
        if (await accountRepository.ChangeEmail(changeEmail))
        {
            toastService.ShowToast("ایمیل جدید ذخیره شد و ایمیل تایید ارسال گردید", Enums.ToastLevel.Success);

            if (userInfo.Email.Trim().ToLower() != changeEmail.Email.Trim().ToLower())
                userInfo.IsEmailApproved = false;

            userInfo.Email = changeEmail.Email;
            await ChangeEmailModal.Close();
        }
    }

    async Task DeleteAccount()
    {

    }
}
