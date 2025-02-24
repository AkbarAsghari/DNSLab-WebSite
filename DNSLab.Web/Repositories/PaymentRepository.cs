using DNSLab.Web.Components.Pages.Defaults;
using DNSLab.Web.DTOs.Repositories.Payment;
using DNSLab.Web.Interfaces.Providers;
using DNSLab.Web.Interfaces.Repositories;

namespace DNSLab.Web.Repositories
{
    public class PaymentRepository(IHttpServiceProvider _HttpServiceProvider) : IPaymentRepository
    {
        const string APIController = "Payment";

        public async Task<IEnumerable<PaymentDTO>?> GetAllPayments(int startIndex, int count)
        {
            return await _HttpServiceProvider.Get<IEnumerable<PaymentDTO>?>($"{APIController}/GetAllPayments?startIndex={startIndex}&count={count}");
        }

        public async Task<int?> GetAllPaymentsCount()
        {
            return await _HttpServiceProvider.Get<int?>($"{APIController}/GetAllPaymentsCount");
        }

        public async Task<IEnumerable<PaymentDTO>?> GetPayments(int startIndex, int count)
        {
            return await _HttpServiceProvider.Get<IEnumerable<PaymentDTO>?>($"{APIController}/GetPayments?startIndex={startIndex}&count={count}");
        }

        public async Task<int?> GetPaymentsCount()
        {
            return await _HttpServiceProvider.Get<int?>($"{APIController}/GetPaymentsCount");
        }

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
