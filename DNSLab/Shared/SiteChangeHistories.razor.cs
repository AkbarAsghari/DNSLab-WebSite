using DNSLab.DTOs.Pages;

namespace DNSLab.Shared;
partial class SiteChangeHistories
{
    IEnumerable<ChangeLogDTO> ChangeLogs;

    protected override async Task OnInitializedAsync()
    {
        ChangeLogs = await pageRepository.GetLastChangeLogs();
    }
}
