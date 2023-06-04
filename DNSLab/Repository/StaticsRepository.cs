using DNSLab.DTOs.Pages;
using DNSLab.DTOs.Statics;
using DNSLab.Enums;
using DNSLab.Interfaces.Helper;
using DNSLab.Interfaces.Repository;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace DNSLab.Repository
{
    public class StaticsRepository : IStaticsRepository
    {
        private readonly IHttpService _httpService;
        private readonly IMemoryCache _memoryCache;
        public StaticsRepository(IHttpService httpService, IMemoryCache memoryCache)
        {
            this._httpService = httpService;
            this._memoryCache = memoryCache;
        }

        public async Task<StatResponse> GetStat(StatTypeEnum type, DateTime? start = null, DateTime? end = null)
        {
            var response = await _httpService.Get<StatResponse>($"/Statics/DNSStat?type={type}" +
                (start != null && end != null ? $"&start={start}&end={end}" : String.Empty));
            if (!response.Success)
                return null;
            else
                return response.Response;
        }

        public async Task<bool> PageVisit(string ip, string url)
        {
            var response = await _httpService.Post<bool>($"/Statics/PageVisit?ip={ip}&pageUrl={url}");
            if (!response.Success)
                return false;
            else
                return response.Response;
        }

        public async Task<int> PageVisitCount(string url)
        {
            var response = await _httpService.Get<int>($"/Statics/PageVisitCount?pageUrl={url}");
            if (!response.Success)
                return 0;
            else
                return response.Response;
        }
    }
}
