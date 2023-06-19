using DNSLab.DTOs.Pages;

namespace DNSLab.Pages.SiteChanges;
partial class EditSiteChange
{
    [Parameter] public Guid ChangeLogId { get; set; }
    private ChangeLogDTO existChangeLog;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            existChangeLog = await pageRepository.GetChangeLog(ChangeLogId);
            await this.InvokeAsync(() => StateHasChanged());
        }
    }

    public async Task Update()
    {
        if (await pageRepository.UpdateChangeLog(existChangeLog))
        {
            _navManager.NavigateTo("ChangeLogs/All");
            Snackbar.Add(localizer["ChangeLogUpdated"], MudBlazor.Severity.Success);
        }
    }
}
