using DNSLab.DTOs.Transactions;
using DNSLab.Interfaces.Helper;
using DNSLab.Interfaces.Repository;

namespace DNSLab.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IHttpService _httpService;
        public TransactionRepository(IHttpService httpService)
        {
            this._httpService = httpService;
        }

        public async Task<string> Tip(TipTransactionDTO request)
        {
            var response = await _httpService.Post<TipTransactionDTO, string>($"/Transaction/Tip", request);
            if (!response.Success)
                return String.Empty;
            else
                return response.Response;
        }

        public async Task<bool> Verify(long trackId)
        {
            var response = await _httpService.Get<bool>($"/Transaction/Verify?trackid={trackId}");
            if (!response.Success)
                return false;
            else
                return response.Response;
        }
    }
}
