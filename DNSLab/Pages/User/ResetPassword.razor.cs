using DNSLab.DTOs.User;

namespace DNSLab.Pages.User;
partial class ResetPassword
{
    [Parameter] public string Token { get; set; }
    private ResetPasswordDTO resetPassword = new ResetPasswordDTO();

    private bool isPasswordChanged = false;

    private async Task ResetUserPassword()
    {
        resetPassword.Token = Token;
        isPasswordChanged = await accountReository.ResetPassword(resetPassword);
    }
}
