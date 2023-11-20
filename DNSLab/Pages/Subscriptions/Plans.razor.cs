using DNSLab.DTOs.Subscriptions;
using DNSLab.Interfaces.Repository;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Pages.Subscriptions;

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
        var result = await _SubscriptionsRepository.Subscription(new DTOs.Transactions.SubscriptionTransactionDTO
        {
            Plan = id
        });

        if (!String.IsNullOrEmpty(result))
            navigationManager.NavigateTo(result);
        _DisabledBuyButton = false;
    }
}
