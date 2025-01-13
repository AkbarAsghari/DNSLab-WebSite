using DNSLab.Web.Interfaces.Providers;
using DNSLab.Web.Interfaces.Repositories;

namespace DNSLab.Web.Repositories
{
    public class DashboardRepository(IHttpServiceProvider _HttpServiceProvider) : IDashboardRepository
    {
        const string APIController = "Dashboard";
        public async Task<int?> GetDDNSsCount()
        {
            return await _HttpServiceProvider.Get<int>($"{APIController}/GetDDNSsCount");
        }

        public async Task<int?> GetRecordsCount()
        {
            return await _HttpServiceProvider.Get<int>($"{APIController}/GetRecordsCount");
        }

        public async Task<int?> GetTodayDDNSIpChangesCount()
        {
            return await _HttpServiceProvider.Get<int>($"{APIController}/GetTodayDDNSIpChangesCount");
        }

        public async Task<int?> GetZonesCount()
        {
            return await _HttpServiceProvider.Get<int>($"{APIController}/GetZonesCount");
        }
    }
}
