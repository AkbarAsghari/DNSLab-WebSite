using DNSLab.DTOs.Tips;
using DNSLab.Enums;
using DNSLab.Interfaces.Helper;
using DNSLab.Interfaces.Repository;
using Microsoft.Extensions.Caching.Memory;

namespace DNSLab.Repository
{
    public class TipRepository : ITipRepository
    {
        private readonly IHttpService _httpService;
        private readonly IMemoryCache _memoryCache;
        public TipRepository(IHttpService httpService, IMemoryCache memoryCache)
        {
            this._httpService = httpService;
            this._memoryCache = memoryCache;
        }

        public async Task<IEnumerable<TipDTO>> GetTips()
        {
            if (!_memoryCache.TryGetValue(CacheKeyEnum.Tips, out IEnumerable<TipDTO> cacheValue))
            {
                var result = await _httpService.Get<IEnumerable<TipDTO>>($"/Tip/GetTips");
                cacheValue = result.Response;
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(3));

                _memoryCache.Set(CacheKeyEnum.Tips, cacheValue, cacheEntryOptions);
            }
            return cacheValue;
        }
    }
}
