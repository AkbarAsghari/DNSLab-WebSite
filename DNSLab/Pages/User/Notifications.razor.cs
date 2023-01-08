using DNSLab.DTOs.User;

namespace DNSLab.Pages.User;

partial class Notifications
{
    private UserInfo userInfo;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            userInfo = await _AccountRepository.Get();
            await this.InvokeAsync(() => StateHasChanged());
        }
    }

    private async Task SaveChanges()
    {
        if (await _AccountRepository.Update(userInfo))
            _ToastService.ShowToast("تغییرات ذخیره شد", Enums.ToastLevel.Success);
    }
}
