using DNSLab.DTOs.Subscriptions;
using DNSLab.Interfaces.Repository;
using DNSLab.Pages.User;
using Microsoft.AspNetCore.Components.Authorization;

namespace DNSLab.Pages.Subscriptions;

partial class BuySubscriptionButton
{
    [Inject] ISubscriptionsRepository SubscriptionsRepository { get; set; }
    [Inject] AuthenticationStateProvider Auth { get; set; }

    bool? _IsSubscipted = null;
    bool _ShowSubscriptionDetails = false;
    protected override async Task OnInitializedAsync()
    {
        var user = (await Auth.GetAuthenticationStateAsync()).User;

        if (user.Identity != null)
        {
            _IsSubscipted = await SubscriptionsRepository.IsSubscripted();
        }
    }

    IEnumerable<SubscriptionInfoDTO> Subscriptions { get; set; } = null;
    async Task ShowSubscriptionDetails()
    {
        _ShowSubscriptionDetails = true;

        Subscriptions = await SubscriptionsRepository.GetActiveSubscriptions();


    }
}
