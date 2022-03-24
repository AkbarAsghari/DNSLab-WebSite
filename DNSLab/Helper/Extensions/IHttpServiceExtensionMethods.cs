using DNSLab.DTOs.Pagination;
using DNSLab.Interfaces.Helper;
using System.Text.Json;

namespace DNSLab.Helper.Extensions
{
    public static class IHttpServiceExtensionMethods
    {
        public static async Task<T> GetHelper<T>(this IHttpService httpService, string url)
        {
            var response = await httpService.Get<T>(url);
            if (!response.Success)
            {
                throw new ApplicationException(await response.HttpResponseMessage.Content.ReadAsStringAsync());
            }

            return response.Response;
        }
        public static async Task<PagedListDTO<T>> GetHelper<T>(this IHttpService httpService, string url , PaginationDTO pagination)
        {
            string newURL = String.Empty;
            if (url.Contains("?"))
                newURL = $"{url}&page={pagination.Page}&recordsPerPage={pagination.RecordsPerPage}";
            else
                newURL = $"{url}?page={pagination.Page}&recordsPerPage={pagination.RecordsPerPage}";

            var response = await httpService.Get<PagedListDTO<T>>(newURL);
            if (!response.Success)
            {
                throw new ApplicationException(await response.HttpResponseMessage.Content.ReadAsStringAsync());
            }

            return response.Response;
        }

    }
}
