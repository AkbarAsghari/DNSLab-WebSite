using DNSLab.DTOs.Subscriptions;
using DNSLab.Interfaces.Repository;

namespace DNSLab.Pages.Subscriptions;

partial class Plans
{
    [Inject] ISubscriptionsRepository _SubscriptionsRepository { get; set; }

    IEnumerable<PlanInfoDTO> PlansInfo;
    protected override async Task OnInitializedAsync()
    {
        PlansInfo = await _SubscriptionsRepository.GetPlans();
    }
}
