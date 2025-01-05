using DNSLab.Web.DTOs.Repositories.Record;
using DNSLab.Web.DTOs.Repositories.Zone;
using DNSLab.Web.Interfaces.Providers;
using DNSLab.Web.Interfaces.Repositories;
using System.Reflection;

namespace DNSLab.Web.Repositories
{
    public class DDNSRepository(IHttpServiceProvider _HttpServiceProvider) : IDDNSRepository
    {
        const string APIController = "DDNS";

        public async Task<bool> AddRecord(Guid zoneId, BaseRecordDTO model)
        {
            return await _HttpServiceProvider.Post<BaseRecordDTO, bool>($"{APIController}/AddRecord?zoneId={zoneId}", model);
        }

        public async Task<IEnumerable<ZoneDTO>?> GetAllZones()
        {
            return await _HttpServiceProvider.Get<IEnumerable<ZoneDTO>?>($"{APIController}/GetAllZones");
        }

        public async Task<IEnumerable<Tuple<ZoneDTO, IEnumerable<BaseRecordDTO>>>?> GetDDNSRecords()
        {
            return await _HttpServiceProvider.Get<IEnumerable<Tuple<ZoneDTO, IEnumerable<BaseRecordDTO>>>?>($"{APIController}/GetDDNSRecords");
        }
    }
}
