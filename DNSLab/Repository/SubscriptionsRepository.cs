using DNSLab.DTOs.DNS;
using DNSLab.DTOs.Subscriptions;
using DNSLab.DTOs.Transactions;
using DNSLab.Interfaces.Helper;
using DNSLab.Interfaces.Repository;
using Microsoft.Extensions.Caching.Memory;

namespace DNSLab.Repository
{
    public class SubscriptionsRepository : ISubscriptionsRepository
    {
        private readonly IHttpService _httpService;

        public SubscriptionsRepository(IHttpService httpService)
        {
            this._httpService = httpService;
        }
        public async Task<IEnumerable<SubscriptionInfoDTO>> GetActiveSubscriptions()
        {
            var response = await _httpService.Get<IEnumerable<SubscriptionInfoDTO>>($"/Subscription/GetActiveSubscriptions");
            return response.Response;
        }

        public async Task<IEnumerable<SubscriptionInfoDTO>> GetAllSubscriptions()
        {
            var response = await _httpService.Get<IEnumerable<SubscriptionInfoDTO>>($"/Subscription/GetAllSubscriptions");
            return response.Response;
        }

        public async Task<IEnumerable<PlanInfoDTO>> GetPlans()
        {
            var response = await _httpService.Get<IEnumerable<PlanInfoDTO>>($"/Subscription/GetPlans");
            return response.Response;
        }

        public async Task<bool> IsSubscripted()
        {
            var response = await _httpService.Get<bool>($"/Subscription/IsSubscripted");
            return response.Response;
        }

        public async Task<string> Subscription(SubscriptionTransactionDTO request)
        {
            var response = await _httpService.Post<SubscriptionTransactionDTO, string>($"/Subscription/Subscription", request);
            if (!response.Success)
                return String.Empty;
            else
                return response.Response;
        }
    }
}
