using DNSLab.Web.DTOs.Repositories.Wallet;
using DNSLab.Web.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Pages.Wallet;

partial class MyWallet
{
    [Inject] IWalletRepository _WalletRepository { get; set; }

    WalletDTO? _Wallet { get; set; }

    protected override async Task OnInitializedAsync()
    {
        _Wallet = await _WalletRepository.GetWallet();
    }
}
