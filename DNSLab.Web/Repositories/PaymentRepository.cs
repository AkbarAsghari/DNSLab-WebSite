using DNSLab.Web.Interfaces.Providers;
using DNSLab.Web.Interfaces.Repositories;

namespace DNSLab.Web.Repositories
{
    public class PaymentRepository(IHttpServiceProvider _HttpServiceProvider) : IPaymentRepository
    {
        const string APIController = "Payment";
        public async Task<string?> RequestPaymentUrl(int amount)
        {
            return await _HttpServiceProvider.Get<string?>($"{APIController}/RequestPaymentUrl?Amount={amount}");
        }

        public async Task<bool> Verify(long trackId)
        {
            return await _HttpServiceProvider.Get<bool>($"{APIController}/Verify?TrackId={trackId}");
        }
    }
}
