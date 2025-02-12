using DNSLab.Web.DTOs.Repositories.Page;
using DNSLab.Web.Interfaces.Providers;
using DNSLab.Web.Interfaces.Repositories;

namespace DNSLab.Web.Repositories
{
    public class PageRepository(IHttpServiceProvider _HttpServiceProvider) : IPageRepository
    {
        const string APIController = "Page";
        public Task<bool> CreatePage(PageDTO model)
        {
            return _HttpServiceProvider.Post<PageDTO, bool>($"{APIController}/CreatePage", model);
        }

        public Task<bool> DeletePage(Guid id)
        {
            return _HttpServiceProvider.Delete<bool>($"{APIController}/DeletePage?id={id}");
        }

        public Task<IEnumerable<PageInfoDTO>?> GetAllPages()
        {
            return _HttpServiceProvider.Get<IEnumerable<PageInfoDTO>?>($"{APIController}/GetAllPages");
        }

        public Task<IEnumerable<PageInfoDTO>?> GetLastPages()
        {
            return _HttpServiceProvider.Get<IEnumerable<PageInfoDTO>?>($"{APIController}/GetLastPages");
        }

        public Task<PageDTO?> GetPage(Guid id)
        {
            return _HttpServiceProvider.Get<PageDTO?>($"{APIController}/GetPage?id={id}");
        }

        public Task<PageDTO?> GetPageByUrl(string url)
        {
            return _HttpServiceProvider.Get<PageDTO?>($"{APIController}/GetPageByUrl?url={url}");
        }

        public Task<bool> UpdatePage(PageDTO model)
        {
            return _HttpServiceProvider.Put<PageDTO, bool>($"{APIController}/UpdatePage", model);
        }
    }
}
