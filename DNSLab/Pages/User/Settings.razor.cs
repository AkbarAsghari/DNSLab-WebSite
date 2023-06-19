using DNSLab.DTOs.User;

namespace DNSLab.Pages.User;

partial class Settings
{
    SettingsDTO settings;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            settings = await _AccountRepository.GetSettings();
            await this.InvokeAsync(() => StateHasChanged());
        }
    }

    private async Task SaveChanges()
    {
        if (await _AccountRepository.UpdateSettings(settings))
            Snackbar.Add("تغییرات ذخیره شد", MudBlazor.Severity.Success);
    }
}
