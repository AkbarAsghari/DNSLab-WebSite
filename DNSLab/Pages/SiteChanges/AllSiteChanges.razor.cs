using DNSLab.DTOs.Pages;

namespace DNSLab.Pages.SiteChanges;
partial class AllSiteChanges
{
    IEnumerable<ChangeLogDTO> ChangeLogs;

    Modal DeleteModal { get; set; }
    ChangeLogDTO deleteRcord { get; set; } = new ChangeLogDTO();

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

    private async Task AcceptDelete()
    {
        if (deleteRcord != null)
        {
            if (await pageRepository.DeleteChangeLog(deleteRcord.ID))
            {
                await DeleteModal.Close();
                await LoadSiteChanges();
            }
        }
        deleteRcord = new ChangeLogDTO();
    }

    private async Task DeleteSiteChange(ChangeLogDTO record)
    {
        deleteRcord = record;
        await DeleteModal.Open();
    }

    private async Task EditSiteChange(ChangeLogDTO record)
    {
        NavigationManager.NavigateTo("ChangeLogs/Edit/" + record.ID);
    }
}
