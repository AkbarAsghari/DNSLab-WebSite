using DNSLab.Web.DTOs.Repositories.Page;

namespace DNSLab.Web.Interfaces.Repositories
{
    public interface IPageRepository
    {
        Task<bool> CreatePage(PageDTO model);
        Task<bool> UpdatePage(PageDTO model);
        Task<bool> DeletePage(Guid id);
        Task<PageDTO?> GetPage(Guid id);
        Task<PageDTO?> GetPageByUrl(string url);
        Task<IEnumerable<PageInfoDTO>?> GetAllPages();
        Task<IEnumerable<PageInfoDTO>?> GetLastPages();
    }
}
