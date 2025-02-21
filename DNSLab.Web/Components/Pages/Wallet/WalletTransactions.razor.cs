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

    bool _IsFirstLoad = true;
    bool _Loading = true;
    private async Task<GridData<WalletTransactionDTO>> ServerDataFunc(GridStateVirtualize<WalletTransactionDTO> gridState, CancellationToken token)
    {
        try
        {
            if (_IsFirstLoad)
            {
                _IsFirstLoad = false;
            }
            else
            {
                await Task.Delay(300, token);
            }

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
        finally { _Loading = false; }

        return new GridData<WalletTransactionDTO>
        {
            Items = [],
            TotalItems = 0
        };
    }
}
