using DNSLab.DTOs.Subscriptions;
using DNSLab.DTOs.Transactions;
using DNSLab.Interfaces.Repository;

namespace DNSLab.Pages.Subscriptions;

partial class AllSubscriptionsTransactions
{
    [Inject] ISubscriptionsRepository SubscriptionsRepository { get; set; }

    IEnumerable<SubscriptionTransactionDTO> SubscriptionTransactions { get; set; } = null;
    protected override async Task OnInitializedAsync()
    {
        SubscriptionTransactions = await SubscriptionsRepository.GetAllSubscriptionTransactions();
    }
}
