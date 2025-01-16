using DNSLab.Web.DTOs.Repositories.Wallet;
using DNSLab.Web.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;
using static MudBlazor.CategoryTypes;

namespace DNSLab.Web.Components.Pages.Wallet;

partial class WalletTransactions
{
    [Inject] IWalletRepository _WalletRepository { get; set; }

    int? _TotalTransactionsCount { get; set; }
    protected override async Task OnInitializedAsync()
    {
        _TotalTransactionsCount = await _WalletRepository.GetWalletTransactionsCount();
    }

    private async Task<GridData<WalletTransactionDTO>> ServerDataFunc(GridStateVirtualize<WalletTransactionDTO> gridState, CancellationToken token)
    {
        try
        {
            await Task.Delay(1000, token);

            var walletTransactions = await _WalletRepository.GetWalletTransactions(gridState.StartIndex, gridState.Count);

            if (walletTransactions != null)
            {
                return new GridData<WalletTransactionDTO>
                {
                    Items = walletTransactions,
                    TotalItems = _TotalTransactionsCount!.Value
                };
            }
        }
        catch { }

        return new GridData<WalletTransactionDTO>
        {
            Items = [],
            TotalItems = 0
        };
    }
}
