using DNSLab.Interfaces.Repository;
using DNSLab.Pages.User;
using Microsoft.AspNetCore.Components.Authorization;

namespace DNSLab.Pages.Subscriptions;

partial class BuySubscriptionButton
{
    [Inject] ISubscriptionsRepository SubscriptionsRepository { get; set; }
    [Inject] AuthenticationStateProvider Auth { get; set; }

    bool _IsSubscipted = false;
    protected override async Task OnParametersSetAsync()
    {
        var user = (await Auth.GetAuthenticationStateAsync()).User;

        if (user.Identity != null)
        {
            _IsSubscipted = await SubscriptionsRepository.IsSubscripted();
        }
    }
}
