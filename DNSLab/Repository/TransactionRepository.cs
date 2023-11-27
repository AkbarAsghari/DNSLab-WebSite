using DNSLab.DTOs.Tips;
using DNSLab.DTOs.Transactions;
using DNSLab.Enums;
using DNSLab.Interfaces.Helper;
using DNSLab.Interfaces.Repository;
using Microsoft.Extensions.Caching.Memory;

namespace DNSLab.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IHttpService _httpService;
        private readonly IMemoryCache _memoryCache;

        public TransactionRepository(IHttpService httpService, IMemoryCache memoryCache)
        {
            this._httpService = httpService;
            _memoryCache = memoryCache;
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
