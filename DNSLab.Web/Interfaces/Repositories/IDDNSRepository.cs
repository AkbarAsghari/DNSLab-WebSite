using DNSLab.Web.DTOs.Repositories.DDNS;
using DNSLab.Web.DTOs.Repositories.Record;
using DNSLab.Web.DTOs.Repositories.Zone;

namespace DNSLab.Web.Interfaces.Repositories
{
    public interface IDDNSRepository
    {
        Task<IEnumerable<ZoneDTO>?> GetAllZones();
        Task<bool> AddRecord(Guid zoneId, BaseRecordDTO model);
        Task<IEnumerable<Tuple<ZoneDTO, IEnumerable<BaseRecordDTO>>>?> GetDDNSDomainAndRecords();

        Task<bool> AddToken(UpdateTokenAndRecordsDTO model);
        Task<bool> UpdateToken(UpdateTokenAndRecordsDTO model);
        Task<bool> DeleteToken(Guid Id);
        Task<string?> RevokeTokenKey(Guid Id);
        Task<IEnumerable<UpdateTokenDTO>?> GetTokens();
        Task<UpdateTokenAndRecordsDTO?> GetToken(Guid Id);
    }
}
