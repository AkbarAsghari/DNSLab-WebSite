using DNSLab.Web.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Pages.Accounts;

partial class ForgetPassword
{
    [Inject] IAccountRepository _AccountRepository { get; set; }

    ForgetPasswordDTO _ForgetPasswordDTO = new();

    bool _ResetPasswordSend = false;
    public async Task SendForgetPasswordMail()
    {
        _ResetPasswordSend = await _AccountRepository.ForgetPasswordAsync(_ForgetPasswordDTO);
    }
}
