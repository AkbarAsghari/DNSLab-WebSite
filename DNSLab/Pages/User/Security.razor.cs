using DNSLab.DTOs.User;

namespace DNSLab.Pages.User;
partial class Security
{
    private ChangePasswordDTO changePassword = new ChangePasswordDTO();

    private async Task SubmitChangePassword()
    {
        if (await accountReository.ChangePassword(changePassword))
        {
            Snackbar.Add("رمز عبور شما با موفقیت تغییر یافت", MudBlazor.Severity.Success);
            navigationManager.NavigateTo("user/info");
        }
    }
}
