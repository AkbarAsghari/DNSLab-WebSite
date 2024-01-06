using DNSLab.DTOs.Subscriptions;
using DNSLab.DTOs.Transactions;

namespace DNSLab.Interfaces.Repository
{
    public interface ISubscriptionsRepository
    {
        Task<IEnumerable<PlanInfoDTO>> GetPlans();
        Task<string> Subscription(SubscriptionDTO request);
        Task<IEnumerable<SubscriptionInfoDTO>> GetActiveSubscriptions();
        Task<IEnumerable<SubscriptionInfoDTO>> GetAllSubscriptions();
        Task<bool> IsSubscripted();
        Task<int> GetActiveSubscriptionCount();
        Task<IEnumerable<SubscriptionTransactionDTO>> GetAllSubscriptionTransactions();
    }
}
