using DNSLab.DTOs.Dashboard;
using DNSLab.DTOs.DNS;
using DNSLab.Interfaces.Helper;
using DNSLab.Interfaces.Repository;
using Microsoft.Extensions.Caching.Memory;

namespace DNSLab.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly IHttpService _httpService;
        private readonly IMemoryCache _memoryCache;

        public DashboardRepository(IHttpService httpService, IMemoryCache memoryCache)
        {
            this._httpService = httpService;
            this._memoryCache = memoryCache;
        }

        public async Task<DashboardDTO> GetStats()
        {
            var response = await _httpService.Get<DashboardDTO>($"/Dashboard/GetStats");
            return response.Response;
        }
    }
}
