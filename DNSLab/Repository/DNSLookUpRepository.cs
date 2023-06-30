using DNSLab.DTOs.DNSLookUp;
using DNSLab.Interfaces.Helper;
using DNSLab.Interfaces.Repository;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace DNSLab.Repository
{
    public class DNSLookUpRepository : IDNSLookUpRepository
    {
        private readonly IHttpService _httpService;
        private readonly IMemoryCache _memoryCache;

        public DNSLookUpRepository(IHttpService httpService, IMemoryCache memoryCache)
        {
            this._httpService = httpService;
            this._memoryCache = memoryCache;
        }

        public async Task<IEnumerable<T>> Query<T>(string query) where T : RecordInfoDTO
        {
            int queryType = 0;

            Type type = typeof(T);

            if (type == typeof(ARecordDTO))
                queryType = 1;
            if (type == typeof(AaaaRecordDTO))
                queryType = 28;
            else if (type == typeof(NsRecordDTO))
                queryType = 2;
            else if (type == typeof(CNAMERecordDTO))
                queryType = 5;
            else if (type == typeof(SOARecordDTO))
                queryType = 6;
            else if (type == typeof(MXRecordDTO))
                queryType = 15;
            else if (type == typeof(TXTRecordDTO))
                queryType = 16;

            var response = await _httpService.Get<IEnumerable<T>>($"/DNSLookUp/Query?query={query}&querytype={queryType}");
            if (response.Success)
                return response.Response;
            else
                return null;
        }

        public async Task<string> QueryReverse(string iPAddress)
        {
            var response = await _httpService.Get<string>($"/DNSLookUp/QueryReverse?ipaddress={iPAddress}");
            if (response.Success)
                return response.Response;
            else
                return String.Empty;
        }
    }
}
