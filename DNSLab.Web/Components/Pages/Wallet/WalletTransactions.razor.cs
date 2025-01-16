using DNSLab.Web.DTOs.Repositories.Wallet;
using DNSLab.Web.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Pages.Wallet;

partial class WalletTransactions
{
    [Inject] IWalletRepository _WalletRepository { get; set; }

    IEnumerable<WalletTransactionDTO>? _WalletTransactions { get; set; }

    bool _IsLoading = false;

    protected override async Task OnInitializedAsync()
    {
        _IsLoading = true;

        _WalletTransactions = await _WalletRepository.GetWalletTransactions();

        _IsLoading = false;
    }
}
