using DNSLab.Web.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Pages.Defaults;

partial class Dashboard
{
    [Inject] IDashboardRepository _DashboardRepository { get; set; }

    int? ZoneCount { get; set; }
    int? RecordsCount { get; set; }
    int? DDNSsCount { get; set; }
    int? TodayDDNSIpChangesCount { get; set; }

    protected override async Task OnInitializedAsync()
    {
        ZoneCount = await _DashboardRepository.GetZonesCount();
        RecordsCount = await _DashboardRepository.GetRecordsCount();
        DDNSsCount = await _DashboardRepository.GetDDNSsCount();
        TodayDDNSIpChangesCount = await _DashboardRepository.GetTodayDDNSIpChangesCount();
    }
}
