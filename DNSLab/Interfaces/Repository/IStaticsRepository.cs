using DNSLab.DTOs.Pages;
using DNSLab.DTOs.Statics;

namespace DNSLab.Interfaces.Repository
{
    public interface IStaticsRepository
    {
        Task<bool> PageVisit(string ip, string url);
        Task<int> PageVisitCount(string url);
        Task<StatResponse> GetStat(StatTypeEnum type, DateTime? start = null, DateTime? end = null);
    }
}
