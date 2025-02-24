using DNSLab.Web.DTOs.Repositories.Payment;
using DNSLab.Web.DTOs.Repositories.Wallet;
using DNSLab.Web.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Pages.Payment;

partial class Payments
{
    [Inject] IPaymentRepository _PaymentRepository { get; set; }

    int? _TotalPaymentsCount { get; set; }
    protected override async Task OnInitializedAsync()
    {
        _TotalPaymentsCount = await _PaymentRepository.GetPaymentsCount();
    }

    bool _IsFirstLoad = true;
    private async Task<GridData<PaymentDTO>> ServerDataFunc(GridStateVirtualize<PaymentDTO> gridState, CancellationToken token)
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

            var walletTransactions = await _PaymentRepository.GetPayments(gridState.StartIndex, gridState.Count);

            if (walletTransactions != null)
            {
                return new GridData<PaymentDTO>
                {
                    Items = walletTransactions,
                    TotalItems = _TotalPaymentsCount!.Value
                };
            }
        }
        catch { }

        return new GridData<PaymentDTO>
        {
            Items = [],
            TotalItems = 0
        };
    }
}
