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

        public async Task<TipsInformationDTO> GetTipsInformation()
        {
            if (!_memoryCache.TryGetValue(CacheKeyEnum.TipsInformation, out TipsInformationDTO cacheValue))
            {
                var result = await _httpService.Get<TipsInformationDTO>($"/Transaction/GetTipsInformation");
                cacheValue = result.Response;
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromSeconds(5));

                _memoryCache.Set(CacheKeyEnum.TipsInformation, cacheValue, cacheEntryOptions);
            }
            return cacheValue;
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
