using DNSLab.Web.DTOs.Repositories.Record;
using DNSLab.Web.Enums;

namespace DNSLab.Web.Interfaces.Repositories
{
    public interface IRecordRepository
    {
        Task<bool> AddRecord(Guid zoneId, BaseRecordDTO model);
        Task<bool> DeleteRecord(RecordTypeEnum type, Guid Id);
        Task<bool> DisableRecord(RecordTypeEnum type, Guid Id);
        Task<IEnumerable<BaseRecordDTO>?> GetRecords(Guid zoneId);
        Task<bool> UpdateRecord(BaseRecordDTO model);
    }
}
