using DNSLab.DTOs.Pages;

namespace DNSLab.Pages.SiteChanges;
partial class CreateSiteChange
{
    private ChangeLogDTO ChangeLog = new ChangeLogDTO { Title = String.Empty };

    public async Task Create()
    {
        if (await pageRepository.AddChangeLog(ChangeLog))
        {
            _navManager.NavigateTo("ChangeLogs/All");
            toastService.ShowToast(localizer["NewChangeLogAdded"], Enums.ToastLevel.Success);
        }
    }
}
