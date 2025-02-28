using DNSLab.Web.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Pages.Accounts;

partial class ResetPassword
{
    [Inject] IAccountRepository _AccountRepository { get; set; }
    [Parameter] public string Token { get; set; }

    ResetPasswordDTO _ResetPasswordDTO = new();

    bool _IsSuccess = false;
    public async Task SavePassword()
    {
        _ResetPasswordDTO.Token = Token;
        _IsSuccess = await _AccountRepository.ResetPasswordAsync(_ResetPasswordDTO);
    }
}
