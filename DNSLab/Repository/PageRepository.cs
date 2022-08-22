using DNSLab.DTOs.Comments;
using DNSLab.DTOs.Pages;
using DNSLab.Enums;
using DNSLab.Interfaces.Helper;
using DNSLab.Interfaces.Repository;
using Microsoft.Extensions.Caching.Memory;

namespace DNSLab.Repository
{
    public class PageRepository : IPageRepository
    {
        private readonly IHttpService _httpService;
        private readonly IMemoryCache _memoryCache;

        public PageRepository(IHttpService httpService, IMemoryCache memoryCache)
        {
            this._httpService = httpService;
            this._memoryCache = memoryCache;
        }

        public async Task<bool> AddNewPage(CreatePageDTO createPage)
        {
            var result = await _httpService.Post<CreatePageDTO, bool>($"/Pages/Create", createPage);
            return result.Response;
        }

        public async Task<bool> DeletePage(Guid id)
        {
            var result = await _httpService.Delete<bool>($"/Pages/Delete?Id={id}");
            return result.Response;
        }

        public async Task<bool> EditPage(PageDTO page)
        {
            var result = await _httpService.Put<PageDTO, bool>($"/Pages/Update", page);
            return result.Response;
        }

        public async Task<IEnumerable<PageSummaryDTO>> GetAllPagesSummary()
        {
            if (!_memoryCache.TryGetValue(CacheKeyEnum.GetAllPagesSummary, out IEnumerable<PageSummaryDTO> cacheValue))
            {
                var result = await _httpService.Get<IEnumerable<PageSummaryDTO>>($"/Pages/GetAllPagesSummary");
                cacheValue = result.Response;
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(3));

                _memoryCache.Set(CacheKeyEnum.GetAllPagesSummary, cacheValue, cacheEntryOptions);
            }

            return cacheValue;
        }

        public async Task<IEnumerable<PageSummaryDTO>> GetAllPagesSummaryByPageType(PageTypeEnum pageTypeEnum)
        {
            string key = $"{CacheKeyEnum.GetAllPagesSummaryByPageType}{pageTypeEnum}";
            if (!_memoryCache.TryGetValue(key, out IEnumerable<PageSummaryDTO> cacheValue))
            {
                var result = await _httpService.Get<IEnumerable<PageSummaryDTO>>($"/Pages/GetAllPagesSummaryByPageType?pageType={pageTypeEnum}");
                cacheValue = result.Response;
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(3));

                _memoryCache.Set(key, cacheValue, cacheEntryOptions);
            }

            return cacheValue;
        }

        public async Task<IEnumerable<string>> GetAllPagesURL()
        {
            var result = await _httpService.Get<IEnumerable<string>>($"/Pages/GetAllPagesURL");
            return result.Response;
        }

        public async Task<PageDTO> GetPage(Guid id)
        {
            var result = await _httpService.Get<PageDTO>($"/Pages/get?Id={id}");
            return result.Response;
        }

        public async Task<PublishPageDTO> GetPageByURL(string url)
        {
            string key = CacheKeyEnum.PublishPage + url;
            if (!_memoryCache.TryGetValue(key, out PublishPageDTO cacheValue))
            {

                var result = await _httpService.Get<PublishPageDTO>($"/Pages/getByURL?url={url}");
                cacheValue = result.Response;
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(3));

                _memoryCache.Set(key, cacheValue, cacheEntryOptions);
            }

            return cacheValue;
        }

        public async Task<PageMetadataDTO> GetPageMetadata(string url)
        {
            var result = await _httpService.Get<PageMetadataDTO>($"/Pages/GetPageMetaData?url={url}");
            return result.Response;
        }

        public async Task<bool> AddChangeLog(ChangeLogDTO ChangeLogs)
        {
            var response = await _httpService.Post<ChangeLogDTO, bool>($"/Pages/AddChangeLog", ChangeLogs);
            if (response.Success)
                return response.Response;

            return false;
        }

        public async Task<bool> UpdateChangeLog(ChangeLogDTO ChangeLogs)
        {
            var response = await _httpService.Put<ChangeLogDTO, bool>($"/Pages/UpdateChangeLog", ChangeLogs);
            if (response.Success)
                return response.Response;

            return false;
        }

        public async Task<bool> DeleteChangeLog(Guid id)
        {
            var response = await _httpService.Delete<bool>($"/Pages/DeleteChangeLog?id={id}");
            if (response.Success)
                return response.Response;

            return false;
        }

        public async Task<ChangeLogDTO> GetChangeLog(Guid id)
        {
            var response = await _httpService.Get<ChangeLogDTO>($"/Pages/GetChangeLog?id={id}");
            if (response.Success)
                return response.Response;

            return null;
        }

        public async Task<IEnumerable<ChangeLogDTO>> GetLastChangeLogs()
        {
            if (!_memoryCache.TryGetValue(CacheKeyEnum.GetLastChanges, out IEnumerable<ChangeLogDTO> cacheValue))
            {
                var result = await _httpService.Get<IEnumerable<ChangeLogDTO>>($"/Pages/GetLastChangeLogs");
                cacheValue = result.Response;
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(3));

                _memoryCache.Set(CacheKeyEnum.GetLastChanges, cacheValue, cacheEntryOptions);
            }

            return cacheValue;
        }

        public async Task<IEnumerable<ChangeLogDTO>> GetAllChangeLogs()
        {
            var result = await _httpService.Get<IEnumerable<ChangeLogDTO>>($"/Pages/GetAllChangeLogs");
            return result.Response;
        }
    }
}
