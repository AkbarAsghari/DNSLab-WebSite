using ApexCharts;
using DNSLab.Web.DTOs.Repositories.Page;
using DNSLab.Web.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Caching.Memory;
using System.Threading;

namespace DNSLab.Web.Components.Pages.Pages;

partial class Blog
{
    [Inject] IPageRepository _PageRepository { get; set; }
    [Inject] IMemoryCache _MemoryCache { get; set; }

    [Parameter] public string? Url { get; set; }

    PageDTO? _Page;
    protected override async Task OnInitializedAsync()
    {
        if (!String.IsNullOrEmpty(Url))
        {
            if (!_MemoryCache.TryGetValue(Url, out PageDTO? cachedResponse))
            {
                cachedResponse = await _PageRepository.GetPageByUrl(Url);
                _MemoryCache.Set(Url, cachedResponse, TimeSpan.FromMinutes(5));
            }
            _Page = cachedResponse;
        }
    }
}
