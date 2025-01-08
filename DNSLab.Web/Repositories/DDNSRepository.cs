using DNSLab.Web.DTOs.Repositories.DDNS;
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

        public async Task<bool> AddToken(UpdateTokenAndRecordsDTO model)
        {
            return await _HttpServiceProvider.Post<UpdateTokenAndRecordsDTO, bool>($"{APIController}/AddToken", model);
        }

        public async Task<bool> DeleteToken(Guid Id)
        {
            return await _HttpServiceProvider.Delete<bool>($"{APIController}/DeleteToken?Id={Id}");
        }

        public async Task<IEnumerable<ZoneDTO>?> GetAllZones()
        {
            return await _HttpServiceProvider.Get<IEnumerable<ZoneDTO>?>($"{APIController}/GetAllZones");
        }

        public async Task<IEnumerable<Tuple<ZoneDTO, IEnumerable<BaseRecordDTO>>>?> GetDDNSDomainAndRecords()
        {
            return await _HttpServiceProvider.Get<IEnumerable<Tuple<ZoneDTO, IEnumerable<BaseRecordDTO>>>?>($"{APIController}/GetDDNSDomainAndRecords");
        }

        public async Task<IEnumerable<BaseRecordDTO>?> GetDDNSRecords()
        {
            return await _HttpServiceProvider.Get<IEnumerable<BaseRecordDTO>?>($"{APIController}/GetDDNSRecords");
        }

        public async Task<UpdateTokenAndRecordsDTO?> GetToken(Guid Id)
        {
            return await _HttpServiceProvider.Get<UpdateTokenAndRecordsDTO?>($"{APIController}/GetToken?Id={Id}");
        }

        public async Task<IEnumerable<UpdateTokenDTO>?> GetTokens()
        {
            return await _HttpServiceProvider.Get<IEnumerable<UpdateTokenDTO>?>($"{APIController}/GetTokens");
        }

        public async Task<string?> RevokeTokenKey(Guid Id)
        {
            return await _HttpServiceProvider.Put<string?>($"{APIController}/RevokeTokenKey?Id={Id}");
        }

        public async Task<bool> UpdateToken(UpdateTokenAndRecordsDTO model)
        {
            return await _HttpServiceProvider.Put<UpdateTokenAndRecordsDTO,bool>($"{APIController}/UpdateToken", model);
        }
    }
}
