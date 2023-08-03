using DNSLab.DTOs.DNS;
using DNSLab.DTOs.Pagination;
using DNSLab.Enums.DNSQueryLogs;

namespace DNSLab.Interfaces.Repository
{
    public interface IDNSRepository
    {
        Task<PagedListDTO<HostNameDTO>> GetOwnHosts(PaginationDTO pagination);
        Task<PagedListDTO<HostNameAndUserDTO>> GetHostNames(PaginationDTO pagination);
        Task<HostNameDTO> GetHostName(Guid hostNameId);
        Task<bool> CreateHostName(CreateHostNameDTO createHostName);
        Task<bool> DeleteHostName(Guid hostNameId);
        Task<bool> UpdateHostName(HostNameDTO createHostName);

        Task<int> GetTodayIPChangesCount();
        Task<IEnumerable<GetLast30DayIPChangesCountDTO>> GetLast30DayIPChangesCount();
        Task<int> GetAllUsersDNSCount();

        Task<IEnumerable<DNSChangeHistoryDTO>> GetDNSChangeHistories();
        Task<IEnumerable<HostSummaryDTO>> GetHostSummaries();
        Task<QueryLogsResponseDTO> QueryLogs(Guid hostId, int pageNumber = 1, int entriesPerPage = 10, bool descendingOrder = true, DateTime? start = null, DateTime? end = null, string? clientIpAddress = null, ProtocolEnum? protocol = null, ResponseTypeEnum? responseType = null, RCodeEnum? rCode = null, RecordTypeEnum? qType = null, ClassEnum? @class = null);

        Task<bool> GenerateTokenForAccessToUpdateHostNameSystem(TokenAndDNSDTO createToken);
        Task<string> RevokeTokenKey(Guid tokenId);
        Task<bool> UpdateTokensDomainNameSystems(TokenAndDNSDTO tokenAndDNS);
        Task<bool> UpdateTokenName(TokenDTO tokenAndName);
        Task<bool> DeleteToken(Guid tokenId);
        Task<IEnumerable<TokenSummaryDTO>> GetTokenSummary();
        Task<TokenAndDNSDTO> GetToken(Guid tokenId);
    }
}
