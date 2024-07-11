using DNSLab.DTOs.Subscriptions;
using DNSLab.Interfaces.Repository;
using DNSLab.Components.Pages.User;
using Microsoft.AspNetCore.Components.Authorization;

namespace DNSLab.Components.Pages.Subscriptions;

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
        else
        {
            _IsSubscipted = false;
        }
    }

    async Task ShowSubscriptionDetails()
    {
        _ShowSubscriptionDetails = true;
    }
}
