namespace DNSLab.Pages.User;
partial class Logout
{
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await authService.Logout();
            navigationManager.NavigateTo("");
            await this.InvokeAsync(() => StateHasChanged());
        }
    }
}
