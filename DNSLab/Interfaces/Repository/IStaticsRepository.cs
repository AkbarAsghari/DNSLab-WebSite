using DNSLab.DTOs.Statics;

namespace DNSLab.Interfaces.Repository
{
    public interface IStaticsRepository
    {
        Task<bool> PageVisit(string ip, string url);
        Task<int> PageVisitCount(string url);
        Task<bool> AddSiteChange(SiteChangesDTO siteChanges);
        Task<bool> UpdateSiteChange(SiteChangesDTO siteChanges);
        Task<bool> DeleteSiteChange(Guid id);
        Task<SiteChangesDTO> GetSiteChange(Guid id);
        Task<IEnumerable<SiteChangesDTO>> GetLastChanges();
        Task<IEnumerable<SiteChangesDTO>> GetAllSiteChanges();
    }
}
