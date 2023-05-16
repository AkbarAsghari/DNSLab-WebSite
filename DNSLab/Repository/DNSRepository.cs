using DNSLab.DTOs.DNS;
using DNSLab.DTOs.Pagination;
using DNSLab.Enums;
using DNSLab.Helper.Extensions;
using DNSLab.Interfaces.Helper;
using DNSLab.Interfaces.Repository;
using Microsoft.Extensions.Caching.Memory;

namespace DNSLab.Repository
{
    public class DNSRepository : IDNSRepository
    {
        private readonly IHttpService _httpService;
        private readonly IMemoryCache _memoryCache;

        public DNSRepository(IHttpService httpService, IMemoryCache memoryCache)
        {
            this._httpService = httpService;
            this._memoryCache = memoryCache;
        }

        public async Task<bool> CreateHostName(CreateHostNameDTO createHostName)
        {
            var response = await _httpService.Post<CreateHostNameDTO, bool>($"/DNS/create", createHostName);
            return response.Response;
        }

        public async Task<bool> DeleteHostName(Guid hostNameId)
        {
            var response = await _httpService.Delete<bool>($"/DNS/Delete?Id={hostNameId}");
            return response.Response;
        }

        public async Task<int> GetActiveDNSCount()
        {
            var response = await _httpService.Get<int>($"/DNS/GetActiveDNSCount");
            if (response.Success)
                return response.Response;

            return 0;
        }

        public async Task<HostNameDTO> GetHostName(Guid hostNameId)
        {
            var response = await _httpService.Get<HostNameDTO>($"/DNS/Get?Id={hostNameId}");
            return response.Response;
        }

        public async Task<int> GetLast24HoursChangesCount()
        {
            var response = await _httpService.Get<int>($"/DNS/GetLast24HoursChangesCount");
            if (response.Success)
                return response.Response;

            return 0;
        }

        public async Task<IEnumerable<DNSChangeHistoryDTO>> GetDNSChangeHistories()
        {
            var response = await _httpService.Get<IEnumerable<DNSChangeHistoryDTO>>($"/DNS/GetDNSChangeHistories");
            if (response.Success)
                return response.Response;

            return new List<DNSChangeHistoryDTO>();
        }

        public async Task<PagedListDTO<HostNameDTO>> GetOwnHosts(PaginationDTO pagination)
        {
            return await _httpService.GetHelper<HostNameDTO>($"/DNS/Getallpaging", pagination);
        }

        public async Task<PagedListDTO<HostNameAndUserDTO>> GetHostNames(PaginationDTO pagination)
        {
            return await _httpService.GetHelper<HostNameAndUserDTO>($"/DNS/GetHostNames", pagination);
        }

        public async Task<IEnumerable<HostSummaryDTO>> GetHostSummaries()
        {
            var response = await _httpService.Get<IEnumerable<HostSummaryDTO>>($"/DNS/GetOwnHostsSummary");
            if (response.Success)
                return response.Response;

            return new List<HostSummaryDTO>();
        }

        public async Task<bool> UpdateHostName(HostNameDTO hostName)
        {
            var response = await _httpService.Put<HostNameDTO, bool>($"/DNS/update", hostName);
            return response.Response;
        }

        //Token 

        public async Task<bool> GenerateTokenForAccessToUpdateHostNameSystem(TokenAndDNSDTO createToken)
        {
            var response = await _httpService.Post<TokenAndDNSDTO, bool>($"/DNS/GenerateTokenForAccessToUpdateHostNameSystem", createToken);
            return response.Response;
        }

        public async Task<bool> UpdateTokensDomainNameSystems(TokenAndDNSDTO tokenAndDNS)
        {
            var response = await _httpService.Put<TokenAndDNSDTO, bool>($"/DNS/UpdateTokensDomainNameSystems", tokenAndDNS);
            return response.Response;
        }

        public async Task<bool> UpdateTokenName(TokenDTO tokenAndName)
        {
            var response = await _httpService.Put<TokenDTO, bool>($"/DNS/UpdateTokenName", tokenAndName);
            return response.Response;
        }

        public async Task<bool> DeleteToken(Guid tokenId)
        {
            var response = await _httpService.Delete<bool>($"/DNS/DeleteToken?tokenId={tokenId}");
            return response.Response;
        }

        public async Task<IEnumerable<TokenSummaryDTO>> GetTokenSummary()
        {
            var response = await _httpService.Get<IEnumerable<TokenSummaryDTO>>($"/DNS/GetTokensSummary");
            if (response.Success)
                return response.Response;

            return new List<TokenSummaryDTO>();
        }

        public async Task<TokenAndDNSDTO> GetToken(Guid tokenId)
        {
            var response = await _httpService.Get<TokenAndDNSDTO>($"/DNS/GetToken?tokenId={tokenId}");
            return response.Response;
        }

        public async Task<string> RevokeTokenKey(Guid tokenId)
        {
            var response = await _httpService.Put<string>($"/DNS/RevokeTokenKey?tokenId={tokenId}");
            return response.Response;
        }

        public async Task<int> GetAllUsersDNSCount()
        {
            if (!_memoryCache.TryGetValue(CacheKeyEnum.GetAllUsersDNSCount, out int cacheValue))
            {
                var result = await _httpService.Get<int>($"/DNS/GetAllUsersDNSCount");
                if (!result.Success)
                    cacheValue = 0;
                else
                    cacheValue = result.Response;
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(30));

                _memoryCache.Set(CacheKeyEnum.GetAllUsersDNSCount, cacheValue, cacheEntryOptions);
            }
            return cacheValue;
        }

        public async Task<int> GetTodayIPChangesCount()
        {
            if (!_memoryCache.TryGetValue(CacheKeyEnum.GetTodayIPChangesCount, out int cacheValue))
            {
                var result = await _httpService.Get<int>($"/DNS/GetTodayIPChangesCount");
                if (!result.Success)
                    cacheValue = 0;
                else
                    cacheValue = result.Response;
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                _memoryCache.Set(CacheKeyEnum.GetTodayIPChangesCount, cacheValue, cacheEntryOptions);
            }
            return cacheValue;
        }

        public async Task<IEnumerable<GetLast30DayIPChangesCountDTO>> GetLast30DayIPChangesCount()
        {
            var response = await _httpService.Get<IEnumerable<GetLast30DayIPChangesCountDTO>>($"/DNS/GetLast30DayIPChangesCount");
            if (response.Success)
                return response.Response;

            return null;
        }
    }
}
