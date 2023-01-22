using DNSLab.DTOs.User;

namespace DNSLab.Pages.User;

partial class Settings
{
    private SettingsDTO settings;

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
            _ToastService.ShowToast("تغییرات ذخیره شد", Enums.ToastLevel.Success);
    }
}
