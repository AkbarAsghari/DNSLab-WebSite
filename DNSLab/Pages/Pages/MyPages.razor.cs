using DNSLab.DTOs.Pages;
using DNSLab.Repository;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MudBlazor;

namespace DNSLab.Pages.Pages;
partial class MyPages
{
    [Inject] private IDialogService DialogService { get; set; }

    IEnumerable<PageSummaryDTO> pageSummaries;

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

    private async Task DeletePage(PageSummaryDTO record)
    {
        bool? result = await DialogService.ShowMessageBox(
           "هشدار",
           $"آیا از حذف صفحه {record.Title} مطمئن هستید؟",
           yesText: "حذف", cancelText: "انصراف");

        if (result == true)
        {
            if (await _PageRepository.DeletePage(record.Id))
            {
                pageSummaries = pageSummaries.Where(x => x.Id != record.Id).ToList();
            }
        }
    }

    private async Task PublishPage(PageSummaryDTO record)
    {
        bool? result = await DialogService.ShowMessageBox(
           "هشدار",
           $"آیا از انتشار صفحه {record.Title} مطمئن هستید؟",
           yesText: "انتشار", cancelText: "انصراف");

        if (result == true)
        {
            if (await _PageRepository.PublishPage(record.Id))
            {
                pageSummaries.First(x => x.Id == record.Id).IsPublieshed = true;
                Snackbar.Add(localizer["PublishedSuccess"], MudBlazor.Severity.Success);
            }
        }
    }

    private async Task EditPage(PageSummaryDTO record)
    {
        NavigationManager.NavigateTo("Page/Edit/" + record.Id);
    }
}
