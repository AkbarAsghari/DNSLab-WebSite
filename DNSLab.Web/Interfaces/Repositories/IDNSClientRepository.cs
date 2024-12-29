using DNSLab.Web.Enums;

namespace DNSLab.Web.Interfaces.Repositories
{
    public interface IDNSClientRepository
    {
        Task<string?> Query(QueryTypeEnum queryType, string query, string? nameServer = null);
        Task<string> QueryReverse(string ip);
    }
}
