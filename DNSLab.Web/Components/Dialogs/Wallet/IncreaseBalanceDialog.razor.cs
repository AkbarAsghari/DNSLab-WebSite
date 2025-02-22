using DNSLab.Web.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Dialogs.Wallet;

partial class IncreaseBalanceDialog
{
    [Inject] IPaymentRepository _PaymentRepository { get; set; }
    [Inject] NavigationManager _NavigationManager { get; set; }

    [CascadingParameter] MudDialogInstance _MudDialog { get; set; }

    int _Amount = 5000; //Toman

    async Task Pay()
    {
        string? paymentUrl = await _PaymentRepository.RequestPaymentUrl(_Amount * 10);

        if (!String.IsNullOrEmpty(paymentUrl))
        {
            _NavigationManager.NavigateTo(paymentUrl, true);
        }
    }
}
