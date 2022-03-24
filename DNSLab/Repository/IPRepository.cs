using DNSLab.DTOs.IP;
using DNSLab.Interfaces.Helper;
using DNSLab.Interfaces.Repository;

namespace DNSLab.Repository
{
    public class IPRepository : IIPRepository
    {
        private readonly IHttpService _httpService;
        public IPRepository(IHttpService httpService)
        {
            this._httpService = httpService;
        }

        public async Task<IPDTO> GetIP()
        {
            var response = await _httpService.Get<IPDTO>("/ip");
            return response.Response;
        }
    }
}
