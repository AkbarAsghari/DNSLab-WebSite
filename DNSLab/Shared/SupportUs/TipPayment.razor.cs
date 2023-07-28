using DNSLab.DTOs.Transactions;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace DNSLab.Shared.SupportUs;

partial class TipPayment
{
    public TipTransactionDTO TipTransaction { get; set; } = new TipTransactionDTO { };

    public IMask amountMask = new RegexMask(@"^\d{0,8}$");

    private async Task Go()
    {
        var result = await _TransactionRepository.Tip(TipTransaction);
        if (!String.IsNullOrEmpty(result))
            navigationManager.NavigateTo(result);
    }
}
