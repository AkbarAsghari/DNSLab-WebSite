using DNSLab.DTOs.Pages;

namespace DNSLab.Pages.Articles;
partial class Articles
{
    IEnumerable<PageSummaryDTO> pageSummaries;

    protected override async Task OnInitializedAsync()
    {
        pageSummaries = await _PageRepository.GetAllPagesSummaryByPageType(PageTypeEnum.Article);
    }
}
