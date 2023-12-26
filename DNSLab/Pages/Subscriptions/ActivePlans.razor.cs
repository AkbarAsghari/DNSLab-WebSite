using DNSLab.DTOs.Subscriptions;
using DNSLab.Interfaces.Repository;
using DNSLab.Repository;

namespace DNSLab.Pages.Subscriptions;

partial class ActivePlans
{
    [Inject] ISubscriptionsRepository SubscriptionsRepository { get; set; }

    IEnumerable<SubscriptionInfoDTO> Subscriptions { get; set; } = null;
    protected override async Task OnInitializedAsync()
    {
        Subscriptions = await SubscriptionsRepository.GetActiveSubscriptions();
    }
}
