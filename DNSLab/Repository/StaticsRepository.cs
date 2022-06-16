using DNSLab.Interfaces.Helper;
using DNSLab.Interfaces.Repository;

namespace DNSLab.Repository
{
    public class StaticsRepository : IStaticsRepository
    {
        private readonly IHttpService _httpService;
        public StaticsRepository(IHttpService httpService)
        {
            this._httpService = httpService;
        }

        public async Task<bool> PageVisit(string ip, string url)
        {
            var response = await _httpService.Post<bool>($"/Statics/PageVisit?ip={ip}&pageUrl={url}");
            if (!response.Success)
                return false;
            else
                return response.Response;
        }

        public async Task<int> PageVisitCount(string url)
        {
            var response = await _httpService.Get<int>($"/Statics/PageVisitCount?pageUrl={url}");
            if (!response.Success)
                return 0;
            else
                return response.Response;
        }
    }
}
