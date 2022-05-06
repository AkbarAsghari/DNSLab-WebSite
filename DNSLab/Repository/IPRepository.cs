using DNSLab.DTOs.DNS;
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

        public async Task<bool?> IPHavePing(string hostOrIPAddress)
        {
            var response = await _httpService.Get<bool>($"/IP/IPHavePing?hostOrIPAddress={hostOrIPAddress}");
            if (response.Success)
                return response.Response;
            else
                return null;
        }

        public async Task<bool?> IsIPAndPortOpen(string hostOrIPAddress, string port)
        {
            var response = await _httpService.Get<bool>($"/IP/IsIPAndPortOpen?hostOrIPAddress={hostOrIPAddress}&port={port}");
            if (response.Success)
                return response.Response;
            else
                return null;
        }
    }
}
