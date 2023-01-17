using DNSLab.DTOs.Pages;
using DNSLab.Enums;

namespace DNSLab.Interfaces.Repository
{
    public interface IPageRepository
    {
        Task<bool> AddNewPage(CreatePageDTO createPage);
        Task<bool> DeletePage(Guid id);
        Task<bool> EditPage(PageDTO page);
        Task<PageDTO> GetPage(Guid id);
        Task<PublishPageDTO> GetPageByURL(string url);
        Task<IEnumerable<string>> GetAllPagesURL();
        Task<IEnumerable<PageSummaryDTO>> GetAllPagesSummary(); 
        Task<IEnumerable<PageSummaryDTO>> GetAllPagesSummaryByPageType(PageTypeEnum pageTypeEnum);
        Task<IEnumerable<PageSummaryDTO>> GetAllPagesSummaryByTag(string tag);
        Task<PageMetadataDTO> GetPageMetadata(string url);
        Task<bool> PublishPage(Guid id);

        Task<bool> AddChangeLog(ChangeLogDTO changeLog);
        Task<bool> UpdateChangeLog(ChangeLogDTO changeLog);
        Task<bool> DeleteChangeLog(Guid id);
        Task<ChangeLogDTO> GetChangeLog(Guid id);
        Task<ChangeLogDTO> GetChangeLogByURL(string url );
        Task<IEnumerable<ChangeLogDTO>> GetLastChangeLogs();
        Task<IEnumerable<ChangeLogDTO>> GetAllChangeLogs();
    }
}
