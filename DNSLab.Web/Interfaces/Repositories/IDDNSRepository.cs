using DNSLab.Web.DTOs.Repositories.Record;
using DNSLab.Web.DTOs.Repositories.Zone;

namespace DNSLab.Web.Interfaces.Repositories
{
    public interface IDDNSRepository
    {
        Task<IEnumerable<ZoneDTO>?> GetAllZones();
        Task<bool> AddRecord(Guid zoneId, BaseRecordDTO model);
        Task<IEnumerable<Tuple<ZoneDTO, IEnumerable<BaseRecordDTO>>>?> GetDDNSRecords();
    }
}
