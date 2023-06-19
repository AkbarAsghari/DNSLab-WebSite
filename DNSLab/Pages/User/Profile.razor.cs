using DNSLab.DTOs.User;

namespace DNSLab.Pages.User;
partial class Profile
{
    UserInfo userInfo;
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            userInfo = await accountRepository.Get();
            await this.InvokeAsync(() => StateHasChanged());
        }
    }

    private async Task SaveChanges()
    {
        if (await accountRepository.Update(userInfo))
        {
            Snackbar.Add("تغییرات ذخیره شد", MudBlazor.Severity.Success);
        }
    }
}
