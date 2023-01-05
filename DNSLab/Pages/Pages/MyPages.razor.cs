using DNSLab.DTOs.Pages;

namespace DNSLab.Pages.Pages;
partial class MyPages
{
    IEnumerable<PageSummaryDTO> pageSummaries;

    Modal pageModal { get; set; }
    bool modalForDeletePage { get; set; }
    PageSummaryDTO modalRecord { get; set; } = new PageSummaryDTO();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await LoadPages();
            await this.InvokeAsync(() => StateHasChanged());
        }
    }

    private async Task LoadPages()
    {
        pageSummaries = await _PageRepository.GetAllPagesSummary();
    }

    private async Task AcceptDelete()
    {
        if (modalRecord != null)
        {
            if (await _PageRepository.DeletePage(modalRecord.Id))
            {
                await pageModal.Close();
                pageSummaries = pageSummaries.Where(x => x.Id != modalRecord.Id).ToList();
            }
        }
        modalRecord = new PageSummaryDTO();
    }

    private async Task AcceptPublish()
    {
        if (modalRecord != null)
        {
            if (await _PageRepository.PublishPage(modalRecord.Id))
            {
                await pageModal.Close();
                pageSummaries.First(x => x.Id == modalRecord.Id).IsPublieshed = true;
                toastService.ShowToast(localizer["PublishedSuccess"], ToastLevel.Success);
            }
        }
        modalRecord = new PageSummaryDTO();
    }

    private async Task DeletePage(PageSummaryDTO record)
    {
        modalForDeletePage = true;
        modalRecord = record;
        await pageModal.Open();
    }

    private async Task PublishPage(PageSummaryDTO record)
    {
        modalForDeletePage = false;
        modalRecord = record;
        await pageModal.Open();
    }

    private async Task EditPage(PageSummaryDTO record)
    {
        NavigationManager.NavigateTo("Page/Edit/" + record.Id);
    }
}
