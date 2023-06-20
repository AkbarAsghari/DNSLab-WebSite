using DNSLab.DTOs.Pages;
using MudBlazor;

namespace DNSLab.Pages.SiteChanges;
partial class AllSiteChanges
{
    [Inject] private IDialogService DialogService { get; set; }

    IEnumerable<ChangeLogDTO> ChangeLogs;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await LoadSiteChanges();
            await this.InvokeAsync(() => StateHasChanged());
        }
    }

    private async Task LoadSiteChanges()
    {
        ChangeLogs = await pageRepository.GetAllChangeLogs();
    }

    private async Task DeleteSiteChange(ChangeLogDTO record)
    {
        bool? result = await DialogService.ShowMessageBox(
            "هشدار",
            $"آیا از حذف تغییر {record.Title} مطمئن هستید؟",
            yesText: "حذف", cancelText: "انصراف");

        if (result == true)
            if (await pageRepository.DeleteChangeLog(record.ID))
            {
                await LoadSiteChanges();
            }
    }

    private async Task EditSiteChange(ChangeLogDTO record)
    {
        NavigationManager.NavigateTo("ChangeLogs/Edit/" + record.ID);
    }
}
