using DNSLab.Web.DTOs.Repositories.Record;
using DNSLab.Web.DTOs.Repositories.Zone;
using DNSLab.Web.Enums;
using DNSLab.Web.Interfaces.Providers;
using DNSLab.Web.Interfaces.Repositories;
using System.Reflection;

namespace DNSLab.Web.Repositories
{
    public class RecordRepository(IHttpServiceProvider _HttpServiceProvider) : IRecordRepository
    {
        const string APIController = "Record";

        public async Task<bool> AddRecord(Guid zoneId, BaseRecordDTO model)
        {
            return await _HttpServiceProvider.Post<BaseRecordDTO, bool>($"{APIController}/AddRecord?ZoneId={zoneId}", model);
        }

        public async Task<bool> DeleteRecord(RecordTypeEnum type, Guid Id)
        {
            return await _HttpServiceProvider.Delete<bool>($"{APIController}/DeleteRecord?Type={(int)type}&Id={Id}");
        }

        public async Task<bool> DisableRecord(RecordTypeEnum type, Guid Id)
        {
            return await _HttpServiceProvider.Put<bool>($"{APIController}/DisableRecord?Type={(int)type}&Id={Id}");
        }

        public async Task<IEnumerable<BaseRecordDTO>?> GetRecords(Guid zoneId)
        {
            return await _HttpServiceProvider.Get<IEnumerable<BaseRecordDTO>?>($"{APIController}/GetRecords?ZoneId={zoneId}");
        }

        public async Task<bool> UpdateRecord(BaseRecordDTO model)
        {
            return await _HttpServiceProvider.Put<BaseRecordDTO, bool>($"{APIController}/UpdateRecord",model);
        }
    }
}
