using DNSLab.DTOs.DNS;
using DNSLab.DTOs.Pagination;
using DNSLab.Helper.Extensions;
using DNSLab.Interfaces.Helper;
using DNSLab.Interfaces.Repository;

namespace DNSLab.Repository
{
    public class DNSRepository : IDNSRepository
    {
        private readonly IHttpService _httpService;
        public DNSRepository(IHttpService httpService)
        {
            this._httpService = httpService;
        }

        public async Task<bool> CreateHostName(CreateHostNameDTO createHostName)
        {
            var response = await _httpService.Post<CreateHostNameDTO, bool>($"/DNS/create", createHostName);
            return response.Response;
        }

        public async Task<bool> DeleteHostName(Guid hostNameId)
        {
            var response = await _httpService.Delete<bool>($"/DNS/Delete?Id={hostNameId}");
            return response.Response;
        }

        public async Task<int> GetActiveDNSCount()
        {
            var response = await _httpService.Get<int>($"/DNS/GetActiveDNSCount");
            if (response.Success)
                return response.Response;

            return 0;
        }

        public async Task<HostNameDTO> GetHostName(Guid hostNameId)
        {
            var response = await _httpService.Get<HostNameDTO>($"/DNS/Get?Id={hostNameId}");
            return response.Response;
        }

        public async Task<int> GetLast24HoursChangesCount()
        {
            var response = await _httpService.Get<int>($"/DNS/GetLast24HoursChangesCount");
            if (response.Success)
                return response.Response;

            return 0;
        }

        public async Task<IEnumerable<DNSChangeHistoryDTO>> GetDNSChangeHistories()
        {
            var response = await _httpService.Get<IEnumerable<DNSChangeHistoryDTO>>($"/DNS/GetDNSChangeHistories");
            if (response.Success)
                return response.Response;

            return new List<DNSChangeHistoryDTO>();
        }

        public async Task<PagedListDTO<HostNameDTO>> GetOwnHosts(PaginationDTO pagination)
        {
            return await _httpService.GetHelper<HostNameDTO>($"/DNS/Getallpaging", pagination);
        }

        public async Task<bool> UpdateHostName(HostNameDTO hostName)
        {
            var response = await _httpService.Put<HostNameDTO, bool>($"/DNS/update", hostName);
            return response.Response;
        }
    }
}
