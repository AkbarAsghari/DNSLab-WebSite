using DNSLab.DTOs.Pages;

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
        Task<PageMetadataDTO> GetPageMetadata(string url);
    }
}
