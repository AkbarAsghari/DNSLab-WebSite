using DNSLab.DTOs.IP;
using DNSLab.DTOs.Pages;

namespace DNSLab.Components.Pages.Pages;
partial class Support
{
    [CascadingParameter] public IPDTO IPDTO { get; set; }

    IEnumerable<PageSummaryDTO> pageSummaries;

    protected override async Task OnInitializedAsync()
    {
        pageSummaries = await _PageRepository.GetAllPagesSummaryByPageType(PageTypeEnum.Support);
    }
}
