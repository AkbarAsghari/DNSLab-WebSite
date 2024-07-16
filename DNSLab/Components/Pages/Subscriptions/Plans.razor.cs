using DNSLab.DTOs.Subscriptions;
using DNSLab.Interfaces.Repository;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace DNSLab.Components.Pages.Subscriptions;

partial class Plans
{
    [Inject] ISubscriptionsRepository _SubscriptionsRepository { get; set; }
    [Inject] NavigationManager navigationManager { get; set; }

    IEnumerable<PlanInfoDTO> PlansInfo;
    protected override async Task OnInitializedAsync()
    {
        PlansInfo = await _SubscriptionsRepository.GetPlans();
    }

    bool _DisabledBuyButton = false;
    async Task Buy(int id)
    {
        _DisabledBuyButton = true;
        var result = await _SubscriptionsRepository.Subscription(new DTOs.Transactions.SubscriptionDTO
        {
            Plan = id
        });

        if (!String.IsNullOrEmpty(result))
        {
            navigationManager.NavigateTo(result);
        }

        if (result == "/")
        {
            Snackbar.Add("نسخه آزمایشی برای شما فعال شد", MudBlazor.Severity.Success);
        }


        _DisabledBuyButton = false;
    }
}
