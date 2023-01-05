using DNSLab.DTOs.Pages;

namespace DNSLab.Pages.Pages;
partial class SearchByTag
{
    IEnumerable<PageSummaryDTO> pageSummaries;

    [Parameter] public string Tag { get; set; }

    protected override async Task OnInitializedAsync()
    {
        pageSummaries = await _PageRepository.GetAllPagesSummaryByTag(Tag);
    }
}
